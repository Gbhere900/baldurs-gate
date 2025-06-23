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
    private RagDollSpawner ragDollSpawner;
    private BaseUnitAction[] baseUnitActionArray;
    [SerializeField] private Transform hitPoint;
    [Header("选中图标")]
    [SerializeField] private GameObject selectedVisual;


    private GridPosition currentGridpostion;
    private GridPosition lastGridpostion ;

    [Header("行动点")]
    [SerializeField] private int MAX_actionPoint = 2;
    [SerializeField] private int actionPoint = 2;
    public  Action OnActionPointChanged;

    [Header("阵营")]
    [SerializeField] private bool isEnemy = false;

    [Header("相机")]
    [SerializeField] private Transform ActionCameraTransform;


    private void Awake()
    {
        unitMove = GetComponent<UnitMove>();
        unitSpin = GetComponent<UnitSpin>();
        health = GetComponent<Health>();
        ragDollSpawner = GetComponent<RagDollSpawner>();
        baseUnitActionArray = GetComponents<BaseUnitAction>();
        LevelGrid.Instance().SetUnitAtGridPosition(this,LevelGrid.Instance().GetGridPosition(transform.position));
        ReSetAtionPoint();
    }

    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged += UpdateSelectedVisual;
        TurnSysterm.Instance().OnTurnCountChanged += TurnSysterm_OnTurnCountChanged;
        health.OnDead += Health_OnDead;

        UpdateSelectedVisual(this,EventArgs.Empty);
    }

    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged -= UpdateSelectedVisual;
        TurnSysterm.Instance().OnTurnCountChanged -= TurnSysterm_OnTurnCountChanged;
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

    private void SpendActionPoint(int cost)
    {
        actionPoint -= cost;
        OnActionPointChanged?.Invoke();
    }

    public bool TrySpendActionPoint(int cost)
    {
        if(actionPoint - cost >=0)
        {
            SpendActionPoint(cost);
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
        OnActionPointChanged.Invoke();
    }

    public void TurnSysterm_OnTurnCountChanged()
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
        return hitPoint;
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    private void Health_OnDead()
    {
        ragDollSpawner.SpawnRagDoll();
        Destroy(gameObject);
    }

    public Transform GetActionCameraTransform()
    {
        return ActionCameraTransform;
    }
}
