using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionUISysterm : MonoBehaviour
{
    [SerializeField] private Button UnitActionButtonPrefabs;
    [SerializeField] private GridLayoutGroup UnitActionButtonContainer;


    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged += UnitActionManager_OnSelectedUnitChanged;
    }
    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged -= UnitActionManager_OnSelectedUnitChanged;
    }
    private void Start()
    {
        UpdateUnitActionButtons();
    }

    public void UpdateUnitActionButtons()
    {
        Unit unit = UnitActionManager.Instance().GetUnit();
        BaseUnitAction[] ActionArray = unit.GetBaseUnitActionArray();
        for (int i = UnitActionButtonContainer.transform.childCount-1; i >= 0; i--)
        {
            GameObject gameObject = UnitActionButtonContainer.transform.GetChild(i).gameObject;
            Destroy(gameObject);
        }
        foreach (BaseUnitAction unitAction in ActionArray)
        {
             UnitActionButton unitActionButton =  GameObject.Instantiate(UnitActionButtonPrefabs, UnitActionButtonContainer.transform).GetComponent<UnitActionButton>();
            unitActionButton.ChangeText(unitAction.GetUnitAcionName());
        }
    }

    private void UnitActionManager_OnSelectedUnitChanged(object sneder,EventArgs empty)
    {
        UpdateUnitActionButtons();
    }
}
