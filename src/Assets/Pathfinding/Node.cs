using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int Coordinates;
    public bool IsWalkable;
    public bool IsExplored;
    public bool IsPath;
    public Node ConnectedTo;

    public Node()
    {
        
    }

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        Coordinates = coordinates;
        IsWalkable = isWalkable;

    }

    public Node(Vector2Int coordinates, bool isWalkable, bool isExplored, bool isPath, Node connectedTo)
    {
        Coordinates = coordinates;
        IsWalkable = isWalkable;
        IsExplored = isExplored;
        IsPath = isPath;
        ConnectedTo = connectedTo;
    }
}
