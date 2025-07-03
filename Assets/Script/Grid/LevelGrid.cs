using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    
    [Header("Íø¸ñ´óÐ¡")]
    [SerializeField] private int lenth = 10;
    [SerializeField] private int width = 10;
    [SerializeField] private int cellSize = 2;

    static private LevelGrid instance;

    [SerializeField] private Transform gridDebugObject;
    private GridSysterm<GridObject> gridSysterm;

    //[SerializeField] private Transform pathFindingGridDebugObject;
    //private GridSysterm<PathFindingGridObject> pathFinding;

    public Action OnUnitGridPositionSwitched;
    public static LevelGrid Instance()
    {
        return instance;
    }
    private void Awake()
    {
        
        instance = this;
        gridSysterm = new GridSysterm<GridObject>(lenth, width, cellSize,
            (GridSysterm<GridObject> g,GridPosition gridPosition) =>new GridObject(g,gridPosition));
        // gridSysterm.CreateDebugObjects(gridDebugObject);
    }

    public void SetUnitAtGridPosition(Unit unit,GridPosition  gridPosition)
    {
        GridObject GridObject = gridSysterm.GetGridObject(gridPosition);
        GridObject.unit = unit;
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject GridObject = gridSysterm.GetGridObject(gridPosition);
        return GridObject.unit;
    }

    public Door GetDoorAtGridPosition(GridPosition gridPosition)
    {
        Door door = gridSysterm.GetDoor(gridPosition);
        return door;
    }

    public void  SetDoorAtGridPosition(Door door ,GridPosition gridPosition)
    {
        GridObject GridObject = gridSysterm.GetGridObject(gridPosition);
        GridObject.door = door;
    }

    public void ClearUnitAtGridPosition( GridPosition gridPosition)
    {
        GridObject GridObject = gridSysterm.GetGridObject(gridPosition);
        GridObject.unit = null;
    }

    public void SwitchUnitFromGridPositionToGridPosition(Unit unit,GridPosition from,GridPosition to)
    {
        ClearUnitAtGridPosition(from);
        SetUnitAtGridPosition(unit, to);
        OnUnitGridPositionSwitched.Invoke();

    }
    public GridPosition  GetGridPosition(Vector3 worldPosition)
    {
        return gridSysterm.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSysterm.GetWorldPosition(gridPosition);
    }
    public bool IsActionGridPositionValid(GridPosition gridPosition)
    {
        return gridSysterm.IsActionGridPositionValid(gridPosition);
    }

    public bool IsGridPositionHasUnit(GridPosition gridPosition)
    {
        return gridSysterm.IsGridPositionHasUnit((GridPosition)gridPosition);
    }

    public int GetGridLenth()

    {
        return lenth;
    }

    public int GetGridWidth()
    {
        return width;
    }
}
