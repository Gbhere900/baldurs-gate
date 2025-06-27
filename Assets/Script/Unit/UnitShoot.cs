using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum ShootState { aim,shoot,cooldown}
public class UnitShoot : BaseUnitAction
{
    private ShootState shootState ;
    private float shootStateTimer = 0;
    private Unit targetUnit;
    [SerializeField] private int damage =4;

    [Header("animator")]
    public Action<Unit> OnStartShootAnimation;
    public override int GetActionPointCost()
    {
        return 1;
    }

    public override int GetMaxActionDistance()
    {
        return 3;
    }

    public override string GetUnitAcionName()
    {
        return "Shoot";
    }

    private void Update()
    {
        if (!isActive)
            return;
        switch (shootState)
        {
            case ShootState.aim:
                Vector3 aimDirection = (targetUnit.transform.position - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward,aimDirection,Time.deltaTime * 5);   //* 5加快旋转速度
                break;
            case ShootState.shoot:
                break;
            case ShootState.cooldown:
                break;
        }
        UpdateShootState();
        
         
    }

    private void UpdateShootState()
    {
        shootStateTimer -= Time.deltaTime;    //减少到0
        if(shootStateTimer < 0)
        {
            switch (shootState)
            {
                case ShootState.aim:
                    float shootTime = 1f;
                    shootStateTimer = shootTime;
                    shootState = ShootState.shoot;
                    OnStartShootAnimation.Invoke(targetUnit);
                    targetUnit.GetComponent<Health>().TakeDamage(damage);
                    Debug.Log("shoot");
                    break;

                case ShootState.shoot:
                    float coolDownTime = 1f;
                    shootStateTimer = coolDownTime;
                    shootState = ShootState.cooldown;
                    Debug.Log("cooldown");
                    break;

                case ShootState.cooldown:
                    ActionEnd();
                    break;
            }
        }


    }
    public override void TakeAcion(GridPosition targetGridPosition, Action OnActionCompeleted)
    {
        //状态机初始化设置
        shootState = ShootState.aim;
        float aimTime = 1f;
        shootStateTimer = aimTime;
        Debug.Log("aim");

        targetUnit = LevelGrid.Instance().GetUnitAtGridPosition(targetGridPosition);
        ActionStart(OnActionCompeleted);

    }

    public override List<GridPosition> GetValidActionGridPosition()
    {
        return GetValidActionGridPosition(unit.GetGridPosition());
    }
    public List<GridPosition> GetValidActionGridPosition(GridPosition gridPosition)
    {
        
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        for (int x = -GetMaxActionDistance(); x <= GetMaxActionDistance(); x++)
        {
            for (int z = -GetMaxActionDistance(); z <= GetMaxActionDistance(); z++)
            {

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = gridPosition + offsetGridPosition;
                if (!(LevelGrid.Instance().IsActionGridPositionValid(testGridPosition)&&LevelGrid.Instance().IsGridPositionHasUnit(testGridPosition)))
                    continue;
                Unit testUnit = LevelGrid.Instance().GetUnitAtGridPosition(testGridPosition);
                //if  符合数组不越界以及格子上有敌对单位
                if (( !testUnit.GetIsEnemy() && unit.GetIsEnemy()
                    || testUnit.GetIsEnemy() && !unit.GetIsEnemy())) 
                {
                    if (x * x + z * z <=GetMaxActionDistance()* GetMaxActionDistance())
                    validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList;
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 100,
        };

    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPosition(gridPosition).Count;
    }
}
