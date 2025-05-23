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
                transform.forward = Vector3.Lerp(transform.forward,aimDirection,Time.deltaTime);
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
    public override void TakeAcion(Vector3 targetPosition, Action OnActionCompeleted)
    {
        //状态机初始化设置
        shootState = ShootState.aim;
        float aimTime = 1f;
        shootStateTimer = aimTime;
        Debug.Log("aim");

        ActionStart(OnActionCompeleted);
        targetUnit = LevelGrid.Instance().GetUnitAtGridPosition(LevelGrid.Instance().GetGridPosition(targetPosition));

    }

    public override List<GridPosition> GetValidActionGridPosition()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        for (int x = -GetMaxActionDistance(); x <= GetMaxActionDistance(); x++)
        {
            for (int z = -GetMaxActionDistance(); z <= GetMaxActionDistance(); z++)
            {

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                //if  符合数组不越界以及格子上有单位
                if (LevelGrid.Instance().IsActionGridPositionValid(testGridPosition) && LevelGrid.Instance().IsGridPositionHasUnit(testGridPosition)) 
                {
                    if (x * x + z * z <=GetMaxActionDistance()* GetMaxActionDistance())
                    validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList;
    }

    
}
