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
    public event EventHandler OnSelectedUnitChanged;

    static public UnitActionManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        if(instance)
        {
            Debug.LogError("instance²»Ö¹Ò»¸ö");
            return;
        }
        instance = this;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleUnitSelection())
            {
                return;
            }

            if(selectedUnit.GetUnitMove().IsGriddPositionvalid(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())))
                selectedUnit.GetUnitMove().Move(LevelGrid.Instance().GetWorldPosition(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())));
        }

        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetUnitSpin().Spin();
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

    public Unit GetUnit()
    {
        return selectedUnit;
    }

}
