using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    private Health health;
    private UnitMove unitMove;
    private UnitSpin unitSpin;
    private BaseUnitAction[] baseUnitActionArray;
    [SerializeField] private Transform hitPoint;
    [Header("选中图标")]
    [SerializeField] private GameObject selectedVisual;


    private GridPosition currentGridpostion;
    private GridPosition lastGridpostion ;

    [Header("行动点")]
    [SerializeField] private int MAX_actionPoint = 2;
    [SerializeField] private int actionPoint = 2;

    [Header("阵营")]
    [SerializeField] private bool isEnemy = false;


    private void Awake()
    {
        unitMove = GetComponent<UnitMove>();
        unitSpin = GetComponent<UnitSpin>();
        health = GetComponent<Health>();
        baseUnitActionArray = GetComponents<BaseUnitAction>();
        LevelGrid.Instance().SetUnitAtGridPosition(this,LevelGrid.Instance().GetGridPosition(transform.position));
        ReSetAtionPoint();
    }

    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged += UpdateSelectedVisual;
        TurnSysterm.Instance().OnTurnCountChanged += TurnSysyerm_OnTurnCountChanged;
        health.OnDead += Health_OnDead;

        UpdateSelectedVisual(this,EventArgs.Empty);
    }

    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged -= UpdateSelectedVisual;
        TurnSysterm.Instance().OnTurnCountChanged -= TurnSysyerm_OnTurnCountChanged;
        health.OnDead -= Health_OnDead;
    }
    void Update()
    {


        currentGridpostion = LevelGrid.Instance().GetGridPosition(transform.position);
        if(currentGridpostion != lastGridpostion)
        {

            LevelGrid.Instance().SwitchUnitFromGridPositionToGridPosition(this,lastGridpostion,currentGridpostion);
            lastGridpostion = currentGridpostion;
        }
    }




    public void DisAbleSelectedVisual()
    {
        selectedVisual.SetActive(false);
    }
    public void EnableSelectedVisual()
    {
        selectedVisual.SetActive(true);
    }

    private void UpdateSelectedVisual(object sender,EventArgs empty)
    {
        Debug.Log("changed");
        if(UnitActionManager.Instance().GetSelectedUnit() == this)
            EnableSelectedVisual();
        else
        {
            DisAbleSelectedVisual();
        }
    }

    public UnitMove GetUnitMove()
    {
        return unitMove;
    }
    public UnitSpin GetUnitSpin()
    {
        return unitSpin;
    }

    public BaseUnitAction[] GetBaseUnitActionArray()
    {

        return baseUnitActionArray; 
    }

    public GridPosition GetGridPosition()
    {
        return currentGridpostion;
    }

    private void SpendActionCost(int cost)
    {
        actionPoint -= cost;
    }

    public bool TrySpendActionCost(int cost)
    {
        if(actionPoint - cost >=0)
        {
            SpendActionCost(cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetActionPoint()
    {
        return actionPoint;
    }

    public void ReSetAtionPoint()
    {
        actionPoint = MAX_actionPoint;
    }

    public void TurnSysyerm_OnTurnCountChanged()
    {
        if(isEnemy&&TurnSysterm.Instance().GetIsEnemyTurn() || 
            !isEnemy && !TurnSysterm.Instance().GetIsEnemyTurn() )
        ReSetAtionPoint();
    }

    public bool GetIsEnemy()
    {
        return isEnemy;
    }
    public Transform GetHitPoint()
    {
        Debug.LogWarning(hitPoint.transform.position);
        return hitPoint;
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    private void Health_OnDead()
    {
        Destroy(gameObject);
    }
}
