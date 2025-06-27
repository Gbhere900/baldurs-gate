using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinding : MonoBehaviour
{

    private const int STRAIGHT_DISTANCE = 10;
    private const int DIAGONAL_DISTANCE = 14;
    [SerializeField] private Transform gridDebugObject;
    [SerializeField] private int length = 10;
    [SerializeField] private int width = 10;
    [SerializeField] private float cellSize = 2f;

    private GridSysterm<PathFindingGridObject> PathFindingGridSysterm;

    private static PathFinding instance;

    private void Awake()
    {
        instance = this;

        PathFindingGridSysterm = new GridSysterm<PathFindingGridObject>(length,width,cellSize,
            (GridSysterm<PathFindingGridObject> g,GridPosition gp) =>  new PathFindingGridObject(g,gp));
        PathFindingGridSysterm.CreateDebugObjects(gridDebugObject);
    }

    public List<PathFindingGridObject> GetShortestPath(GridPosition start,GridPosition end)
    {
        PathFindingGridObject startNode = PathFindingGridSysterm.GetGridObject(start);
        PathFindingGridObject endNode = PathFindingGridSysterm.GetGridObject(end);
        List<PathFindingGridObject> openList = new List<PathFindingGridObject>();
        List<PathFindingGridObject> closeList = new List<PathFindingGridObject>();

        
        for (int i = 0;i<length;i++)
        {
            for(int j = 0;j<width;j++)
            {
                GridPosition tempGridPosition = new GridPosition(i,j);
                PathFindingGridObject node = PathFindingGridSysterm.GetGridObject(tempGridPosition);
                node.ClearLastPathFindingGridObject();
                node.SetG(int.MaxValue);
                node.SetH( CalculateDistance(tempGridPosition,end));
            }
        }

        startNode.SetG(0);
        startNode.SetH(CalculateDistance(start, end));
        openList.Add(startNode);

        while (openList.Count>0)
        {
            PathFindingGridObject currentNode = GetBestNode(openList);
            openList.Remove(currentNode);
            closeList.Add(currentNode);
            if(currentNode == endNode)
            {
                return TrackShortestPath(endNode);
            }
            foreach(var node in GetNeighborNode(currentNode,closeList))
            {

                if(!closeList.Contains(node))
                {
                    node.SetG(currentNode.GetG() + CalculateDistance(currentNode.GetGridPosition(), node.GetGridPosition()));
                    node.SetLastPathFindingGridObject(currentNode);
                    openList.Add(node);
                }
                
            }

        }

        Debug.Log("不存在到此处的路径");
        return null;


    }
    
    private List<PathFindingGridObject> GetNeighborNode(PathFindingGridObject currentNode, List<PathFindingGridObject> closeList)
    {
        List<PathFindingGridObject> NeighborNodeList = new List<PathFindingGridObject>();
        for(int i = -1;i <=1;i++)
        {
            for(int j = -1;j<=1;j++)
            {
                GridPosition currentPosition = currentNode.GetGridPosition();
                GridPosition neighborGridPosition = new GridPosition(currentPosition.x + i,currentPosition.z + j);
                if (PathFindingGridSysterm.IsActionGridPositionValid(neighborGridPosition))
                {
                    PathFindingGridObject neighborNode = PathFindingGridSysterm.GetGridObject(neighborGridPosition);
                    neighborNode.SetG(currentNode.GetG());
                    NeighborNodeList.Add(neighborNode);
                }
            }
        }
        return NeighborNodeList;
    }
    private PathFindingGridObject GetBestNode(List<PathFindingGridObject> openList)
    {
        PathFindingGridObject bestNode = openList[0];
        foreach(var node in openList)
        {
            if(node.GetF() < bestNode.GetF())
            {
                bestNode = node;
            }
        }
        return bestNode;
    }
    private int CalculateDistance(GridPosition start, GridPosition end)
    {
        int distance = 0;
        int xDistance =Mathf.Abs(start.x - end.x);
        int zDistance =Mathf.Abs(start.z - end.z);
        int delta =Math.Min(xDistance, zDistance);
        distance = delta * DIAGONAL_DISTANCE + (Math.Max(xDistance,zDistance) - delta) * STRAIGHT_DISTANCE;
        return distance;
    }

    private List<PathFindingGridObject> TrackShortestPath(PathFindingGridObject endNode)
    {
        List<PathFindingGridObject> ans = new List<PathFindingGridObject>();
        PathFindingGridObject currentNode = endNode;
        ans.Add(currentNode);
        while (currentNode.GetLastPathFindingGridObject() != null) 
        {
            ans.Add(currentNode.GetLastPathFindingGridObject());
            currentNode = currentNode.GetLastPathFindingGridObject();
        }
        ans.Reverse();
        return ans;
    }
    
    public static PathFinding Instance()
    {
        return instance; 
    }
}
