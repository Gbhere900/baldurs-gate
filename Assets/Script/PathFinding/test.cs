using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 last;
    private Vector3 current;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!LevelGrid.Instance().IsActionGridPositionValid(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())))
                return;
            foreach (GridPosition gridposition in PathFinding.Instance().GetShortestPath(new GridPosition(0, 0),
                LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())))
            {
                Gizmos.DrawCube(LevelGrid.Instance().GetWorldPosition(gridposition), new Vector3(0.3f, 0.3f, 0.3f));
            }
        }

    }
}
