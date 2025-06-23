using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject actionCamera;


    private void Start()
    {
        actionCamera.SetActive(false);
    }
    private void OnEnable()
    {
        BaseUnitAction.OnAnyActionStarted += BaseUnitAction_OnAnyActionStarted;
        BaseUnitAction.OnAnyActionEnded += BaseUnitAction_OnAnyActionEnded;
    }
    private void OnDisable()
    {
        BaseUnitAction.OnAnyActionStarted -= BaseUnitAction_OnAnyActionStarted;
        BaseUnitAction.OnAnyActionEnded -= BaseUnitAction_OnAnyActionEnded;
    }

    private void BaseUnitAction_OnAnyActionStarted(BaseUnitAction unitAction)
    {
        switch (unitAction)
        {
            case UnitShoot unitShoot:
                //计算方向
                Vector3 forward = unitShoot.GetTargetUnit().transform.position.normalized;
                
                //设置位置
                Vector3 unitPosition = unitAction.GetUnit().transform.position;
                Vector3 heightOffset = Vector3.up * 1.7f;
                float shoulderOffsetValue = 1f ;
                Vector3 shoulderOffser = Vector3.Cross( Vector3.up, forward) * shoulderOffsetValue - forward * 1;
                Vector3 position = unitPosition + heightOffset + shoulderOffser;
                actionCamera.transform.position = position;

                //设置方向
                actionCamera.transform.LookAt(unitShoot.GetTargetUnit().transform.position + heightOffset);

                ShowActionCamera();
                break;
        }
    }

    private void BaseUnitAction_OnAnyActionEnded(BaseUnitAction unitAction)
    {
        switch (unitAction)
        {
            case UnitShoot unitShoot:
                HideActionCamera();
                break;
        }
     
    }

    private void ShowActionCamera()
    {
        actionCamera.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCamera.SetActive(false);
    }
}
