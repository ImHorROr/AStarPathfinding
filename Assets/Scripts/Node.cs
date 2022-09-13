using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public int gridX, gridY;
	public int gCost, hCost;
	
	public int fCost{get { return gCost + hCost;
		
	}}
	
	public Node parent;
    public bool walkable;
    public Vector3 worldNodePos;
	public Node(bool _walkable , Vector3 _worldNodePos , int _gridX , int _gridY)
	{
		gridX = _gridX ;
		gridY = _gridY;
        walkable = _walkable;
        worldNodePos = _worldNodePos;
    }

}
