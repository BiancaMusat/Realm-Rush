using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Block startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Block> grid = new Dictionary<Vector2Int, Block>();
    Queue<Block> queue = new Queue<Block>();
    bool isRunning = true;

    List<Block> path = new List<Block>();

    Vector2Int[] directions = {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};

    public List<Block> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            ColorStartAndEnd();
            BFS();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);
        Block prev = endWaypoint.exploredFrom;
        while(prev != startWaypoint)
        {
            path.Add(prev);
            prev = prev.exploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }

    private void BFS()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            if (searchCenter == endWaypoint)
            {
                isRunning = false;
            }
            ExploreNeighbours(searchCenter);
        }
    }

    private void ExploreNeighbours(Block searchCenter)
    {
        if (!isRunning)
        {
            return;
        }
        foreach (Vector2Int dir in directions)
        {
            if (grid.ContainsKey(searchCenter.GetGridPos() + dir))
            {
                Block neighbour = grid[searchCenter.GetGridPos() + dir];
                if (!(neighbour.isExplored || queue.Contains(neighbour)))
                {
                    queue.Enqueue(neighbour);
                    neighbour.exploredFrom = searchCenter;
                }
            }
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.blue);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Block>();
        foreach (Block waypoint in waypoints)
        {
            bool isOveralpping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOveralpping)
            {
                Debug.LogWarning(waypoint + " is overlapping");
            }
            else {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }
}
