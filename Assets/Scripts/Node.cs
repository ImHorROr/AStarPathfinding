using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldNodePos;
    public Node(bool _walkable , Vector3 _worldNodePos)
    {
        walkable = _walkable;
        worldNodePos = _worldNodePos;
    }

}
