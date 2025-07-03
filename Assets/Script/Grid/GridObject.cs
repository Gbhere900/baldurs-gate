 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject 
{
    private GridSysterm<GridObject> gridSysterm;
    private GridPosition gridPosition;
    public Unit unit { get; set; }
    public Door door { get; set; }
    public GridObject(GridSysterm<GridObject> gridSysterm,GridPosition gridPosition)
    {
        this.gridSysterm = gridSysterm;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString() + "\n"+ unit;
    }

}
