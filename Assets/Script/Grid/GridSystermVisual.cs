using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystermVisual : MonoBehaviour
{
    [SerializeField] private GridSystermVisualSingle gridSystermVisualSinglePrefabs;
    GridSystermVisualSingle[,] gridSystermVisualSingleArray;

    private void Start()
    {
        gridSystermVisualSingleArray = new GridSystermVisualSingle[LevelGrid.Instance().GetGridWidth(), LevelGrid.Instance().GetGridLenth()];
        for (int x=0;x< LevelGrid.Instance().GetGridWidth();x++)
        {
            for(int z = 0;z<LevelGrid.Instance().GetGridLenth();z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridSystermVisualSingleArray[x, z] = GameObject.Instantiate(gridSystermVisualSinglePrefabs, LevelGrid.Instance().GetWorldPosition(gridPosition), Quaternion.identity);
                
            }
        }

    }
    private void Update()
    {
        HideAllSingles();
        List<GridPosition> validGridPositions = UnitActionManager.Instance().GetUnit().GetUnitMove().GetValidActionGridPosition();
        ShowSingles(validGridPositions);
    }

    public void HideAllSingles()
    {
        foreach(GridSystermVisualSingle single in gridSystermVisualSingleArray)
        {
            single.Hide();
        }
    }

    public void ShowSingles(List<GridPosition> validActionGridPosition)
    {
        foreach(GridPosition gridPosition in validActionGridPosition)
        {
            gridSystermVisualSingleArray[gridPosition.x,gridPosition.z].Show();
        }
    }

}
