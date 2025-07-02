using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : BaseUnitAction
{
    [Header("walk")]
    [SerializeField] private List<GridPosition> targetGridPositionList;
    private int targetPositionIndex = 0;

    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float speed = 1;

    [Header("rotate")]
    //[SerializeField] private Animator animator;
    [SerializeField] private float rotateSpeed = 10f;

    [Header("animator")]
    public Action OnStartWalkAnimation;
    public Action OnStopWalkAnimation;


    protected override void Awake()
    {
        base.Awake();
        targetGridPositionList = new List<GridPosition>();

    }
    private void Update()
    {
        if (!isActive)
            return;
        Vector3 targetPosition = LevelGrid.Instance().GetWorldPosition(targetGridPositionList[targetPositionIndex]);

        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * speed;
           // animator.SetBool("isWalking", true);
            OnStartWalkAnimation.Invoke();
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
        else
        {
            targetPositionIndex++;
            if (targetPositionIndex >= targetGridPositionList.Count)
            {
                StopMove();
            }
           
        }
    }

    public override void TakeAcion(GridPosition targetGridPosition, Action OnActionCompeleted)
    {
        targetPositionIndex = 0;    
        targetGridPositionList.Clear();
        targetGridPositionList = PathFinding.Instance().
        GetShortestPath(unit.GetGridPosition(),targetGridPosition);

        ActionStart(OnActionCompeleted);
    }

    private  void StopMove()
    {
        OnStopWalkAnimation.Invoke();
        ActionEnd();
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

                //if 符合数组不越界以及格子上有单位
                if (!LevelGrid.Instance().IsActionGridPositionValid(testGridPosition))
                {
                    continue;
                    
                }
                if(LevelGrid.Instance().IsGridPositionHasUnit(testGridPosition))
                {
                    continue;
                }
                if (!PathFinding.Instance().isGridPositionCanWalk(testGridPosition))
                {
                    continue;
                }
                validGridPositionList.Add(testGridPosition);

            }
        }
        return validGridPositionList;
    }

    public override string GetUnitAcionName()
    {
        return "Move";
    }

    public override int GetActionPointCost()
    {
        return 1;
    }

    public override int GetMaxActionDistance()
    {
        return 3;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetUnitAction<UnitShoot>().GetTargetCountAtPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGridPosition * 10,
        };

    }
}
