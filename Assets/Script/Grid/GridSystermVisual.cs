using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystermVisual : MonoBehaviour
{
    [SerializeField] private GridSystermVisualSingle gridSystermVisualSinglePrefabs;
    GridSystermVisualSingle[,] gridSystermVisualSingleArray;

    enum GridColor
    {
        White,
        Blue,
        Yellow,
        Red,
        Redsoft
    }
    [Serializable]
    struct GridColorType
    {
       public GridColor gridColor;
        public Material gridMaterial;
    }

    [SerializeField] private List<GridColorType> gridColorTypeList;
    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitActionChanged += UnitActionManager_OnSelectedUnitActionChanged;
        LevelGrid.Instance().OnUnitGridPositionSwitched += LevelGrid_OnUnitGridPositionSwitched;
    }

    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitActionChanged -= UnitActionManager_OnSelectedUnitActionChanged;
        LevelGrid.Instance().OnUnitGridPositionSwitched -= LevelGrid_OnUnitGridPositionSwitched;
    }
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

        BaseUnitAction unitAction = UnitActionManager.Instance().GetSelectedUnitAction();
        GridColor gridColor;
        switch (unitAction)
        {
            case (UnitMove):
                gridColor = GridColor.White; break;
            case (UnitSpin):
                gridColor = GridColor.White; break;
            case (UnitGrenade):
                gridColor = GridColor.Yellow;break;
            case (UnitInteract):
                gridColor = GridColor.White; break;
            case (UnitShoot unitShoot):
                gridColor = GridColor.Red;
                List<GridPosition> possibleShootGridPositions = 
                    GetPossibleGridPositions(unitShoot.GetMaxActionDistance(), unitShoot.GetUnit().GetGridPosition());
                ShowValidSingles(possibleShootGridPositions, GridColor.Redsoft);
                break;
            case (UnitSlash unitSlash):
                List<GridPosition> possibleSlashGridPositions =
                    GetPossibleGridPositions(unitSlash.GetMaxActionDistance(), unitSlash.GetUnit().GetGridPosition());
                ShowValidSingles(possibleSlashGridPositions, GridColor.Redsoft);
                gridColor = GridColor.Red; break;
            default:
                gridColor = GridColor.White; break;
        }

        
        List<GridPosition> validGridPositions = UnitActionManager.Instance().GetSelectedUnitAction().GetValidActionGridPosition();

        ShowValidSingles(validGridPositions,gridColor);

    }
    private void HideAllSingles()
    {
        foreach(GridSystermVisualSingle single in gridSystermVisualSingleArray)
        {
            single.Hide();
        }
    }

    private void ShowValidSingles(List<GridPosition> validActionGridPosition,GridColor gridColor)
    {
        foreach(GridPosition gridPosition in validActionGridPosition)
        {
            Material gridMaterial = GetGridMatetial(gridColor);
            gridSystermVisualSingleArray[gridPosition.x,gridPosition.z].Show(gridMaterial);
        }
    }

    private void UnitActionManager_OnSelectedUnitActionChanged()
    {
        UpdateVisual();
    }

    private void LevelGrid_OnUnitGridPositionSwitched()
    {
        UpdateVisual();
    }

    private Material GetGridMatetial(GridColor gridColor)
    {
        foreach(GridColorType gridColorType in gridColorTypeList)
        {
            if(gridColor == gridColorType.gridColor)
            {
                return gridColorType.gridMaterial;
            }
        }
        Debug.LogError("未找到当前颜色" + gridColor);
        return null;
    }

    private List<GridPosition> GetPossibleGridPositions(int distance,GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        for (int x = -distance; x <= distance ; x++)
        {
            for (int z = -distance; z <= distance; z++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(z) > distance)
                    continue;
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (LevelGrid.Instance().IsActionGridPositionValid(testGridPosition))
                {
                    validGridPositionList.Add(testGridPosition);
                }

            }
        }
        return validGridPositionList ;
    }
}
