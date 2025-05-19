using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSysterm 
{
    int lenth = 1;
    int width = 1;
    float cellSize = 1;
    public GridSysterm(int lenth,int width,float cellSize)
    {
        this.lenth = lenth;
        this.width = width;
        this.cellSize = cellSize;
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < lenth; j++)
            {
                Debug.DrawLine(GetWorldPosition(j,i), GetWorldPosition(j,i) + Vector3.right * 0.2f, Color.blue, 1000f);
            }
        }
        
    }
    public Vector3 GetWorldPosition(int x,int z)
    {
        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    public GridPosition GetGridPosition(Vector3 WorldPosition)
    {
        GridPosition gridPosition = new GridPosition(Mathf.RoundToInt(WorldPosition.x/cellSize), Mathf.RoundToInt(WorldPosition.z/cellSize));
        return gridPosition;
    }


}
