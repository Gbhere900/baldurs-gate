using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSysterm<TGridObject> 
{
    int lenth = 1;
    int width = 1;
    float cellSize = 1;
    private TGridObject[,] gridObjectArray;
    public GridSysterm(int lenth, int width, float cellSize, Func<GridSysterm<TGridObject>, GridPosition, TGridObject>createGridObject)
    {
        this.lenth = lenth;
        this.width = width;
        this.cellSize = cellSize;
        gridObjectArray = new TGridObject[width,lenth];
        for (int z = 0; z < width; z++) 
        {
            for (int x = 0; x < lenth; x++)
            {
                //TGridObject gridObject  = new TGridObject(this,new GridPosition(x,z));
                //gridObjectArray[x,z] = gridObject;
                gridObjectArray[x,z] = createGridObject(this, new GridPosition(x, z));
            }
        }
        
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x * cellSize + cellSize/2, 0, gridPosition.z * cellSize + cellSize/2);
    }

    public GridPosition GetGridPosition(Vector3 WorldPosition)
    {
        GridPosition gridPosition = new GridPosition(Mathf.FloorToInt(WorldPosition.x/cellSize), Mathf.FloorToInt(WorldPosition.z/cellSize));
        return gridPosition;
    }

    public void CreateDebugObjects(Transform debugPrefabs)
    {
        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < lenth; x++)
            {
                GridPosition newGridPosition = new GridPosition(x, z);
                Transform debugPrefabTransform = GameObject.Instantiate(debugPrefabs,GetWorldPosition(newGridPosition),Quaternion.identity);
                GridDebugObject newGridDebugObject =  debugPrefabTransform.GetComponent<GridDebugObject>();
                newGridDebugObject.SetGridObject(gridObjectArray[x,z]);
            }
        }
    }

    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsActionGridPositionValid(GridPosition gridPosition)
    {
            if(gridPosition.x<0||
                gridPosition.z<0||
                gridPosition.x>=width||
                gridPosition.z>=lenth )
                return false;

            return true;
       
        
    }

    public bool IsGridPositionHasUnit(GridPosition gridPosition)
    {
        GridObject gridObject = GetGridObject(gridPosition) as GridObject;
        if (gridObject.unit == null)
            return false;
        else
            return true;
    }
}
