using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void OnEnable()
    {
        UnitActionManager.Instance().OnTakeAction += UnitActionManager_OnTakeAction;
        UnitActionManager.Instance().OnSelectedUnitChanged += UnitActionManager_OnTakeAction;


    }
    private void OnDisable()
    {
        UnitActionManager.Instance().OnTakeAction -= UnitActionManager_OnTakeAction;
        UnitActionManager.Instance().OnSelectedUnitChanged -= UnitActionManager_OnTakeAction;

    }
    private void Start()
    {
        UpdateActionPointText();
    }

    //直接update暴力解决
    private void Update()
    {
        UpdateActionPointText();
    }

    public void UpdateActionPointText()
    {
        textMeshProUGUI.text = "Action Point: " + UnitActionManager.Instance().GetActionPoint();
    }

    public void UnitActionManager_OnTakeAction(object sender,EventArgs e)
    {
        UpdateActionPointText();
    }
}
