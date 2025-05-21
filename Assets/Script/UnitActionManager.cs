using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionManager : MonoBehaviour
{
    static private UnitActionManager instance;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Unit selectedUnit;
    private BaseUnitAction selectedUniAction;
    private bool isBusy  = false;
    public Action OnActionCompeleted;
    public event EventHandler<bool> OnIsBusyChanged; 

    public event EventHandler OnSelectedUnitChanged;
    public Action OnSelectedUnitActionChanged;

    static public UnitActionManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        SetSelectedUnit(selectedUnit);
        if(instance)
        {
            Debug.LogError("instance²»Ö¹Ò»¸ö");
            return;
        }
        instance = this;
    }


    private void Update()
    {
        if (isBusy)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleUnitSelection())
            {
                return;
            }
            
            HandleSelectedUnitAction();
        }
    }
    private void HandleSelectedUnitAction()
    {
        if (selectedUniAction.IsGriddPositionvalid(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())))
        {
            SetIsBusy();
            selectedUniAction.TakeAcion(LevelGrid.Instance().GetWorldPosition(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())), ClearIsBusy);
        }
    }

       


    public bool HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, mask))
        {
            SetSelectedUnit(rayCastHit.transform.gameObject.GetComponent<Unit>());

            return true;
        }
        else
        {
            return false;
        }
          
    }

    private void SetIsBusy()
    {
        isBusy = true;
        OnIsBusyChanged.Invoke(this, true);
    }
    private void ClearIsBusy()
    {
        isBusy = false;
        OnIsBusyChanged.Invoke(this, false);
    }

    public void SetSelectedUniAction(BaseUnitAction baseUnitAction)
    {
        this.selectedUniAction = baseUnitAction;
        OnSelectedUnitActionChanged?.Invoke();
    }
    public BaseUnitAction GetSelectedUnitAction()
    {
        return selectedUniAction;
    }


    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

        SetSelectedUniAction(selectedUnit.GetComponent<UnitMove>());
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

}
