using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField, Tooltip("World grid size - Should match UnityEditor snap settings")] private int unityGridSize = 10;
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid => grid;
    public int UnityGridSize => unityGridSize;

    private void Awake()
    {
        CreatedGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        return grid.ContainsKey(coordinates) ? grid[coordinates] : null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].IsWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(var entry in grid)
        {
            entry.Value.ConnectedTo = null;
            entry.Value.IsExplored = false;
            entry.Value.IsPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        var position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    private void CreatedGrid()
    {
        for(var x = 0; x < gridSize.x; x++)
        {
            for(var y = 0; y < gridSize.y; y++)
            {
                var coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}
