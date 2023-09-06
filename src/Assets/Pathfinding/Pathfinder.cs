using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    public Vector2Int StartCoordinates => startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates => destinationCoordinates;

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    private void ExploreNeighbors()
    {
        var neighbors = new List<Node>();

        foreach (var direction in directions)
        {
            var neighborCoordinates = currentSearchNode.Coordinates + direction;

            if (grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);
            }
        }

        foreach (var neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.Coordinates) && neighbor.IsWalkable)
            {
                neighbor.ConnectedTo = currentSearchNode;
                reached.Add(neighbor.Coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.IsWalkable = true;
        destinationNode.IsWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.IsExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.Coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        var currentNode = destinationNode;
        path.Add(currentNode);
        currentNode.IsPath = true;

        while (currentNode.ConnectedTo != null)
        {
            currentNode = currentNode.ConnectedTo;
            path.Add(currentNode);
            currentNode.IsPath = true;
        }

        path.Reverse();

        return path;
    }


    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].IsWalkable;
            grid[coordinates].IsWalkable = false;
            var newPath = GetNewPath();
            grid[coordinates].IsWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
