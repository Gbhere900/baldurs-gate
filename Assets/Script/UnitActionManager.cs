using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionManager : MonoBehaviour
{
    static private UnitActionManager instance;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Unit selectedUnit;
    static public UnitActionManager Instance()
    {
        return instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleUnitSelection())
            {
                return;
            }
            selectedUnit.Move(MousePositionManager.GetMousePosition());
        }
        
    }

    public bool HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, mask))
        {
            selectedUnit = rayCastHit.transform.gameObject.GetComponent<Unit>();
            return true;
        }
        else
        {
            return false;
        }
        
        
    }

}
