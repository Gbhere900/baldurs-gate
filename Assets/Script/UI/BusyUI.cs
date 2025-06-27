using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusyUI : MonoBehaviour
{
    [SerializeField] private Image image;
    private void Start()
    {
        ChangeBusyUIVisibility(this,false);
    }
    private void OnEnable()
    {
        UnitActionManager.Instance().OnIsBusyChanged += ChangeBusyUIVisibility;

    }
    private void OnDisable()
    {
        UnitActionManager.Instance().OnIsBusyChanged -= ChangeBusyUIVisibility;

    }
    private void  ChangeBusyUIVisibility(object sender, bool isBusy)
    {
        image.gameObject.SetActive(isBusy);
    }
}
