using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class UnitUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointTMP;
    [SerializeField] private Image healthBar_Fill;
    private Unit unit;
    private Health health;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        health = unit.GetComponent<Health>();
        
    }
    private void OnEnable()
    {
        unit.OnActionPointChanged += Unit_OnSpentActionPoint;
        health.OnTakenDamage += Health_OnTakenDamage;
        UpdateActionPointTMP();
        UpdateHealthBarFill();
    }

    private void Health_OnTakenDamage()
    {
        UpdateHealthBarFill();  
    }

    //更新血条进度条
    private void UpdateHealthBarFill()
    {
        healthBar_Fill.fillAmount = health.GetHealthIdentified();
    }


    private void Unit_OnSpentActionPoint ()
    {
        UpdateActionPointTMP();
    }
    //更新行动点UI
    private void UpdateActionPointTMP()
    {
        actionPointTMP.text = unit.GetActionPoint().ToString();
    }


}
