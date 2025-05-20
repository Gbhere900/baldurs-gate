using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    [Header("walk")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float speed = 1;
    [SerializeField] private int maxMoveDistance = 1;

    [Header("animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float rotateSpeed = 10f;

    private Unit unit;

    private void Awake()
    {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * speed;
            animator.SetBool("isWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

    }

    public bool IsGriddPositionvalid(GridPosition gridPosition)
    {
        return GetValidActionGridPosition().Contains(gridPosition);
    }
    public List<GridPosition> GetValidActionGridPosition()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {

                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if(LevelGrid.Instance().IsActionGridPositionValid(testGridPosition))
                {
                    Debug.Log(testGridPosition);
                    validGridPositionList.Add(testGridPosition);
                }
                
            }
        }

        
        return validGridPositionList;
    }

}
