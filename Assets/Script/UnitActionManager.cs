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

    public event EventHandler OnSelectedUnitChanged;

    static public UnitActionManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        selectedUniAction = selectedUnit.GetComponent<UnitMove>();
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
            selectedUniAction.TakeAcion(LevelGrid.Instance().GetWorldPosition(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())), ClearIsBusy);
        }
    }

       


    public bool HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, mask))
        {

            selectedUnit = rayCastHit.transform.gameObject.GetComponent<Unit>();
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
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
    }
    private void ClearIsBusy()
    {
        isBusy = false;
    }

    public void SetSelectedUniAction(BaseUnitAction baseUnitAction)
    {
        this.selectedUniAction = baseUnitAction;
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseUnitAction GetSelectedUnitAction()
    {
        return selectedUniAction;
    }
}
