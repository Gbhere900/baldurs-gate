using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 last;
    private Vector3 current;

    private void OnDrawGizmos()
    {
        if (!LevelGrid.Instance().IsActionGridPositionValid(LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())))
            return;
       foreach(var node in PathFinding.Instance().GetShortestPath(new GridPosition(0, 0), 
           LevelGrid.Instance().GetGridPosition(MousePositionManager.GetMousePosition())))
        {
            Gizmos.DrawCube(LevelGrid.Instance().GetWorldPosition(node.GetGridPosition()), new Vector3(0.3f,0.3f,0.3f));
        }
        
    }
}
