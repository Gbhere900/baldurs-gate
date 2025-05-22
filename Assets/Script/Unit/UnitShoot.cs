using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitShoot : BaseUnitAction
{
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

         
    }

    public override void TakeAcion(Vector3 targetPosition, Action OnActionCompeleted)
    {
        this.OnActionCompeleted = OnActionCompeleted;
        isActive = true;
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
                    validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList;
    }

    
}
