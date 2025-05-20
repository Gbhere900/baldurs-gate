using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpin : BaseUnitAction
{
    [SerializeField] private float rotateSpeed = 1f;
    private float rotateAmount = 0;
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
    public void Spin()
    {
        rotateAmount = 0;
        isActive = true;
    }

    public void StopSpin()
    {
        isActive = false;
    }

}
