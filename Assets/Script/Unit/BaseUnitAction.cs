using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive = false;
    protected Action OnActionCompeleted;
    public static Action<BaseUnitAction> OnAnyActionStarted;
    public static Action<BaseUnitAction> OnAnyActionEnded;
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract String GetUnitAcionName();

    public abstract void TakeAcion(GridPosition targetGrifPosition, Action OnActionCompeleted);

    public  virtual bool IsGriddPositionvalid(GridPosition gridPosition)
    {
        return GetValidActionGridPosition().Contains(gridPosition);
    }
    public  virtual List<GridPosition> GetValidActionGridPosition()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        for (int x = -GetMaxActionDistance(); x <= GetMaxActionDistance(); x++)
        {
            for (int z = -GetMaxActionDistance(); z <= GetMaxActionDistance(); z++)
            {

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (LevelGrid.Instance().IsActionGridPositionValid(testGridPosition))
                {
                    validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList;
    }

    public abstract int GetActionPointCost();

    public abstract int GetMaxActionDistance();

    protected void ActionStart(Action OnActionCompeleted)
    {
        isActive = true;
        this.OnActionCompeleted = OnActionCompeleted;
        OnAnyActionStarted.Invoke(this);
    }

    protected  void ActionEnd()
    {
        isActive =false;
        OnActionCompeleted();
        OnAnyActionEnded.Invoke(this);
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionList = new List<EnemyAIAction>();
        List<GridPosition> validActionGridPositionList = GetValidActionGridPosition();

        foreach(GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
            enemyAIActionList.Add(enemyAIAction);
        }
        if(enemyAIActionList.Count > 0 )
        {
            enemyAIActionList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAIActionList[0];
        }
        else
            return null;
        
    }

    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);

}
