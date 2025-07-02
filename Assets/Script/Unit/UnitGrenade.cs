using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrenade : BaseUnitAction
{
    [SerializeField] private Grenade grenade;
    [SerializeField] private float damage = 20;

    protected override void Awake()
    {
        base.Awake();
    }
    public override int GetActionPointCost()
    {
        return 1;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction()
        {
            gridPosition = gridPosition,
            actionValue = 100
        };

    }

    public override int GetMaxActionDistance()
    {
        return 4;
    }

    public override string GetUnitAcionName()
    {
        return "Grenade";
    }

    public override void TakeAcion(GridPosition targetGrifPosition, Action OnActionCompeleted)
    {
        Grenade grenade =  GameObject.Instantiate<Grenade>(this.grenade,transform);
        grenade.SetUp(targetGrifPosition,damage,ActionEnd);
        
        ActionStart(OnActionCompeleted);
        
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
                //if  符合数组不越界
                {
                    if (x * x + z * z <= GetMaxActionDistance() * GetMaxActionDistance())
                        validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList;
    }
}
