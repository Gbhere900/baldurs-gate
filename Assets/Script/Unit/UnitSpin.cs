using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpin : BaseUnitAction
{
    [SerializeField] private float rotateSpeed = 1f;
    private float rotateAmount = 0;
    protected override void Awake()
    {
        base.Awake();

    }
    private void Update()
    {
        if (!isActive)
            return;
        HandleSpin();

    }

    private void HandleSpin()
    {
        Debug.Log("spin");
        Vector3 rotateOffset = new Vector3(0, rotateSpeed, 0);
        transform.eulerAngles += rotateOffset * Time.deltaTime;
        rotateAmount += rotateSpeed * Time.deltaTime;

        if (rotateAmount > 360)
        {
            
            StopSpin();
        }
    }
    public override void TakeAcion(GridPosition targetGridPosition, Action OnActionCompeleted)
    {
        rotateAmount = 0;
        ActionStart(OnActionCompeleted);
    }

    public void StopSpin()
    {
       ActionEnd();
    }

    public override string GetUnitAcionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPosition()
    {
        return  new List<GridPosition>()
        {
            unit.GetGridPosition()
        };

    }

    public override int GetActionPointCost()
    {
        return 2;
    }

    public override int GetMaxActionDistance()
    {
        return 0;
    }
}
