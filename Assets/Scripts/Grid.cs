﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public GameObject walls;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;
	public List<Vector3> prevPos = new List<Vector3>();
	public List<GameObject> obs = new List<GameObject>();

	void Start()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid();
		GetObjectsInLayer(walls,7);
	}
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
        for (int i = 0; i < prevPos.Count; i++)
        {
			if(obs[i].transform.position.x != prevPos[i].x)
            {
				CreateGrid();
            }
        }
	}
	private void GetObjectsInLayer(GameObject root, int layer)
 {
	 foreach (Transform t in root.GetComponentInChildren<Transform>())
	 {
		 if (t.gameObject.layer == layer)
		 {
			 prevPos.Add(t.gameObject.transform.localPosition);
			 obs.Add(t.gameObject);
		 }
	 }
 }

	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft =
			transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right *
				(x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x, y] = new Node(walkable, worldPoint ,x,y);
			}
		}
	}

	public List<Node> GetNeighboursNodes(Node node)
	{
		List<Node> Neighbours =  new List<Node>();
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0	) continue;
				
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					Neighbours.Add(grid[checkX,checkY]);
				}
				
			}		
		}
		return Neighbours;
	}
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

	public List<Node> path;
    void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));


		if (grid != null)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				if(path != null)
				{
					if(path	.Contains(n))
					{
						Gizmos.color = Color.green;
					}
					
				}
				
				Gizmos.DrawCube(n.worldNodePos, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}
