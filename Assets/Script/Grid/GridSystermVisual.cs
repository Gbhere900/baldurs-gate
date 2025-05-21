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
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        HideAllSingles();
        List<GridPosition> validGridPositions = UnitActionManager.Instance().GetSelectedUnitAction().GetValidActionGridPosition();
        ShowValidSingles(validGridPositions);

    }
    private void HideAllSingles()
    {
        foreach(GridSystermVisualSingle single in gridSystermVisualSingleArray)
        {
            single.Hide();
        }
    }

    private void ShowValidSingles(List<GridPosition> validActionGridPosition)
    {
        foreach(GridPosition gridPosition in validActionGridPosition)
        {
            gridSystermVisualSingleArray[gridPosition.x,gridPosition.z].Show();
        }
    }

}
