using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObject;
    [Header("Íø¸ñ´óÐ¡")]
    [SerializeField] private int lenth = 10;
    [SerializeField] private int width = 10;
    [SerializeField] private int cellSize = 2;

    static private LevelGrid instance;
    private GridSysterm gridSysterm;

    public static LevelGrid Instance()
    {
        return instance;
    }
    private void Awake()
    {
        
        instance = this;
        gridSysterm = new GridSysterm(lenth, width, cellSize);
        gridSysterm.CreateDebugObjects(gridDebugObject);
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

    public void ClearUnitAtGridPosition( GridPosition gridPosition)
    {
        GridObject GridObject = gridSysterm.GetGridObject(gridPosition);
        GridObject.unit = null;
    }

    public void SwitchUnitFromGridPositionToGridPosition(Unit unit,GridPosition from,GridPosition to)
    {
        ClearUnitAtGridPosition(from);
        SetUnitAtGridPosition(unit, to);

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

    public int GetGridLenth()

    {
        return lenth;
    }

    public int GetGridWidth()
    {
        return width;
    }
}
