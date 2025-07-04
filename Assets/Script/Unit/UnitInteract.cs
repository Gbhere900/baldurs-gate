using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInteract : BaseUnitAction
{
    public override int GetActionPointCost()
    {
        return 1;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction { gridPosition = gridPosition, actionValue = 0 };
    }

    public override int GetMaxActionDistance()
    {
        return 1;
    }

    public override string GetUnitAcionName()
    {
        return "Interact";
    }
    protected override void Awake()
    {
        base.Awake();
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
                if (!(LevelGrid.Instance().IsActionGridPositionValid(testGridPosition)))
                    continue;
                if (LevelGrid.Instance().GetInteractableAtGridPosition(gridPosition)==null)
                {
                    Debug.Log("Door为空");
                    continue;
                }
                   
                //if  符合数组不越界
                        validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }
    public override void TakeAcion(GridPosition targetGrifPosition, Action OnActionCompeleted)
    {
        Interactable interactable = LevelGrid.Instance().GetInteractableAtGridPosition(targetGrifPosition);
        interactable.Interact(OnActionCompeleted);
        ActionStart(OnActionCompeleted);
    }
}
