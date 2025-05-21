using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{

    private UnitMove unitMove;
    private UnitSpin unitSpin;
    private BaseUnitAction[] baseUnitActionArray;
    [Header("Ñ¡ÖÐÍ¼±ê")]
    [SerializeField] private GameObject selectedVisual;


    private GridPosition currentGridpostion;
    private GridPosition lastGridpostion ;


    private void Awake()
    {
        unitMove = GetComponent<UnitMove>();
        unitSpin = GetComponent<UnitSpin>();
        baseUnitActionArray = GetComponents<BaseUnitAction>();
        LevelGrid.Instance().SetUnitAtGridPosition(this,LevelGrid.Instance().GetGridPosition(transform.position));
        
    }

    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged += UpdateSelectedVisual;
        UpdateSelectedVisual(this,EventArgs.Empty);
    }

    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged -= UpdateSelectedVisual;
    }
    void Update()
    {


        currentGridpostion = LevelGrid.Instance().GetGridPosition(transform.position);
        if(currentGridpostion != lastGridpostion)
        {

            LevelGrid.Instance().SwitchUnitFromGridPositionToGridPosition(this,lastGridpostion,currentGridpostion);
            lastGridpostion = currentGridpostion;
        }
    }




    public void DisAbleSelectedVisual()
    {
        selectedVisual.SetActive(false);
    }
    public void EnableSelectedVisual()
    {
        selectedVisual.SetActive(true);
    }

    private void UpdateSelectedVisual(object sender,EventArgs empty)
    {
        Debug.Log("changed");
        if(UnitActionManager.Instance().GetUnit() == this)
            EnableSelectedVisual();
        else
        {
            DisAbleSelectedVisual();
        }
    }

    public UnitMove GetUnitMove()
    {
        return unitMove;
    }
    public UnitSpin GetUnitSpin()
    {
        return unitSpin;
    }

    public BaseUnitAction[] GetBaseUnitActionArray()
    {

        return baseUnitActionArray; 
    }

    public GridPosition GetGridPosition()
    {
        return currentGridpostion;
    }
}
