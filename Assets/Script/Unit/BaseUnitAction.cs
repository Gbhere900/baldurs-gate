using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive = false;
    protected Action OnActionCompeleted;
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}
