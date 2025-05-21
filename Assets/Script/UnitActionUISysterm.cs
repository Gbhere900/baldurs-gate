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
    private List<UnitActionButton> unitActionButtonList;

    private void Awake()
    {
        unitActionButtonList = new List<UnitActionButton>();
    }

    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged += UnitActionManager_OnSelectedUnitChanged;
        UnitActionManager.Instance().OnSelectedUnitActionChanged += UnitActionManager_OnSelectedUnitActionChanged;
    }
    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged -= UnitActionManager_OnSelectedUnitChanged;
        UnitActionManager.Instance().OnSelectedUnitActionChanged -= UnitActionManager_OnSelectedUnitActionChanged;
    }
    private void Start()
    {
        UpdateUnitActionButtons();
    }

    public void UpdateUnitActionButtons()
    {
        Unit unit = UnitActionManager.Instance().GetSelectedUnit();
        BaseUnitAction[] ActionArray = unit.GetBaseUnitActionArray();
        for (int i = UnitActionButtonContainer.transform.childCount-1; i >= 0; i--)
        {
            GameObject gameObject = UnitActionButtonContainer.transform.GetChild(i).gameObject;
            Destroy(gameObject);
        }

        unitActionButtonList.Clear();  //清空列表中的对象

        foreach (BaseUnitAction unitAction in ActionArray)
        {
             UnitActionButton unitActionButton =  GameObject.Instantiate(UnitActionButtonPrefabs, UnitActionButtonContainer.transform).GetComponent<UnitActionButton>();
             unitActionButton.Initialize(unitAction);
             unitActionButtonList.Add(unitActionButton);
        }
    }

    private void UnitActionManager_OnSelectedUnitChanged(object sneder,EventArgs empty)
    {
        UpdateUnitActionButtons();
    }

    private void ChangeSelectedShadowVisual(BaseUnitAction baseUnitAction)
    {
        foreach (UnitActionButton unitActionButton in unitActionButtonList)
        {
            unitActionButton.ChangeSelectedVisual(baseUnitAction);
        }
    }

    private void UnitActionManager_OnSelectedUnitActionChanged()
    {
        ChangeSelectedShadowVisual(UnitActionManager.Instance().GetSelectedUnitAction());
    }
}
