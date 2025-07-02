using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingGridObject
{
    [SerializeField] private bool isWalkable = true;
    private GridSysterm<PathFindingGridObject> pathFindingGridObject;
    private GridPosition gridPosition;
    private float h;
    private float g;
    private float f;
    private PathFindingGridObject lastPathFindingGridObject;

    public PathFindingGridObject(GridSysterm<PathFindingGridObject> p,GridPosition gp)
    {
        this.pathFindingGridObject = p;
        gridPosition = gp;
    }

    public float GetH()
    {

    return h; 
    }

    public float GetG()
    {
        return g;
    }

    public float GetF()
    {
        return f;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public void SetH(float h)
    {
        this.h = h;
        calculateF();
    }
    public void SetG(float g)
    {
        this.g = g;
        calculateF();
    }
    public void calculateF()
    {
        f = h + g;
    }

    public void ClearLastPathFindingGridObject()
    {
        lastPathFindingGridObject = null;
    }

    public PathFindingGridObject GetLastPathFindingGridObject()
    {
        return lastPathFindingGridObject;
    }
    public void SetLastPathFindingGridObject(PathFindingGridObject lastPathFindingGridObject)
    {
        this.lastPathFindingGridObject = lastPathFindingGridObject;
    }
    public GridPosition GetGridPosition()
    {

    return gridPosition; 
    }

    public bool GetIsWalkable()
    {
        return isWalkable;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }
}
