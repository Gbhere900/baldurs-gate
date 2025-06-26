using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }

    private State state;
    private float timer;
    private void Awake()
    {
        state = State.WaitingForEnemyTurn;
    }

    private void OnEnable()
    {
        TurnSysterm.Instance().OnTurnCountChanged += TurnSysterm_OnTurnChanged;
    }
    private void Update()
    {
        if(!TurnSysterm.Instance().GetIsEnemyTurn())
        {
            return;
        }

        switch (state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if(timer <=0)
                {
                    state = State.Busy;
                    if(TryTakeEnemyAIAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        TurnSysterm.Instance().NextTurn();
                    }
                   // TurnSysterm.Instance().NextTurn();
                }
                break;
            case State.Busy:
                break;
        }

    }

    private void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }
    private void TurnSysterm_OnTurnChanged()
    {
        if(TurnSysterm.Instance().GetIsEnemyTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }
        
    }

    private bool TryTakeEnemyAIAction(Action onEnemyAIActionCompeleted)
    {
        foreach(Unit enemyUnit in UnitManager.Instance().GetEnemyUnitList())
        {
            if(TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionCompeleted))

            {
                return true;
            }
        }
        return false;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionCompeleted)
    {
        EnemyAIAction bestEnemyAIAction = null;
        BaseUnitAction bestUnitAction = null;
        foreach(BaseUnitAction baseUnitAction in enemyUnit.GetBaseUnitActionArray())
        {
            if (enemyUnit.GetActionPoint() < baseUnitAction.GetActionPointCost()
                || baseUnitAction.GetBestEnemyAIAction()== null)
            {
                continue;
            }
            else
            {
                if (bestEnemyAIAction == null)
                {
                    bestEnemyAIAction = baseUnitAction.GetBestEnemyAIAction();
                    bestUnitAction = baseUnitAction;
                }
                else
                {
                    if (bestEnemyAIAction.actionValue < baseUnitAction.GetBestEnemyAIAction().actionValue)
                    {
                        bestEnemyAIAction = baseUnitAction.GetBestEnemyAIAction();
                        bestUnitAction = baseUnitAction;
                    }
                        
                }
            }
        }
        if (bestEnemyAIAction == null)
            return false; 
        else
        {
            enemyUnit.TrySpendActionPoint(bestUnitAction.GetActionPointCost());
            bestUnitAction.TakeAcion(bestEnemyAIAction.gridPosition, onEnemyAIActionCompeleted);  
            return true;
        }
            
    }
}
