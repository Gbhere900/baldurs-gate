using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SlashState { beforeHit,afterHit }
public class UnitSlash : BaseUnitAction
{

    private SlashState slashState = SlashState.beforeHit;

    private float slashStateTimer = 0f;

    private Unit targetUnit;

    public Action OnStartSlashAnimation;

    [SerializeField] private float damage = 6f;

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
        return new EnemyAIAction{ actionValue = 200, gridPosition = gridPosition };
    }

    public override int GetMaxActionDistance()
    {
        return 1;
    }



    public override string ToString()
    {
        return "Sword";
    }

    public override List<GridPosition> GetValidActionGridPosition()
    {
        GridPosition gridPosition = unit.GetGridPosition();
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        for (int x = -GetMaxActionDistance(); x <= GetMaxActionDistance(); x++)
        {
            for (int z = -GetMaxActionDistance(); z <= GetMaxActionDistance(); z++)
            {

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = gridPosition + offsetGridPosition;
                if (!(LevelGrid.Instance().IsActionGridPositionValid(testGridPosition) && 
                    LevelGrid.Instance().IsGridPositionHasUnit(testGridPosition)))
                    continue;
                Unit testUnit = LevelGrid.Instance().GetUnitAtGridPosition(testGridPosition);
                //if  符合数组不越界以及格子上有敌对单位
                if ((!testUnit.GetIsEnemy() && unit.GetIsEnemy()
                    || testUnit.GetIsEnemy() && !unit.GetIsEnemy()))
                {
                    //if (x * x + z * z <= GetMaxActionDistance() * GetMaxActionDistance())
                        validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList;
    }
    public override string GetUnitAcionName()
    {
        return "Slash";
    }

    public override void TakeAcion(GridPosition targetGrifPosition, Action OnActionCompeleted)
    {
        isActive = true;
        slashState = SlashState.beforeHit;
        targetUnit = LevelGrid.Instance().GetUnitAtGridPosition(targetGrifPosition);
        slashStateTimer = 1f;
        ActionStart(OnActionCompeleted);
        OnStartSlashAnimation.Invoke();
    }

    private void Update()
    {
        if (!isActive)
            return;
        switch (slashState)
        {
            case SlashState.beforeHit:
                Vector3 aimDirection = (targetUnit.transform.position - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 5);   //* 5加快旋转速度
                break;
            case SlashState.afterHit:
                break;

        }
        UpdateShootState();
    }

    private void UpdateShootState()
    {
        slashStateTimer -= Time.deltaTime;    //减少到0
        if (slashStateTimer < 0)
        {
            switch (slashState)
            {
                case SlashState.beforeHit:
                    float beforeSlashTimer = 1f;
                    slashStateTimer = beforeSlashTimer;
                    slashState = SlashState.afterHit;
                    //OnStartShootAnimation.Invoke(targetUnit);
                    targetUnit.GetComponent<Health>().TakeDamage((int)damage);
                    CameraShake.Instance().Shake();
                    break;

                case SlashState.afterHit:
                    ActionEnd();
                    break;      
            }
        }


    }
    public Unit GetTargetUnit()
    {
        return targetUnit;
    }
}
