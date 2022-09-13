using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
	Grid grid;
	public Transform player;
	public Transform target;
	protected void Awake()
	{
		grid = GetComponent<Grid>();
	}
	void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		List<Node> openedSet = new	List<Node>();
		HashSet<Node> closedSet = new	HashSet<Node>();
		
		Node startNode = grid.NodeFromWorldPoint(startPos);
		openedSet.Add(startNode);
		Node endNode = grid.NodeFromWorldPoint(targetPos);
		while (openedSet.Count > 0)
		{
			Node currentNode = openedSet[0];
			for (int i = 0; i < openedSet.Count; i++) {
				if(openedSet[i].fCost < currentNode.fCost ||
					openedSet[i].fCost == currentNode.fCost && openedSet[i].hCost < currentNode.hCost)
				{
					currentNode = openedSet[i];
					
				}
				openedSet.Remove(currentNode);
				closedSet.Add(currentNode);
				if(currentNode == endNode)
				{
					RetractPath(startNode , endNode);
					return;
				}
				foreach (Node neighnour in grid.GetNeighboursNodes(currentNode))
				{
					if(!neighnour.walkable || closedSet.Contains(neighnour)) continue;
					int movementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighnour);
					if(movementCostToNeighbour < neighnour.gCost || !openedSet.Contains(neighnour) )
					{
						neighnour.gCost = movementCostToNeighbour;
						neighnour.hCost = GetDistance(neighnour, endNode);
						neighnour.parent = currentNode;
						if(!openedSet.Contains(neighnour))
						{
							openedSet.Add(neighnour);
						}							
					}								
				}
			}
		}	
	}
	
	int GetDistance(Node a, Node b) 
	{
		int dstX = Mathf.Abs(a.gridX - b.gridX);
		int dstY = Mathf.Abs(a.gridY - b.gridY);
		if(dstX > dstY)
		{
			return 14 * dstY + 10*(dstX - dstY);
		}
		return 14* dstX + 10*(dstY - dstX);
	}
	
	void RetractPath(Node startNode , Node endNode)
	{
		List<Node> path = new	List<Node>();
		Node currentNode = endNode;
		
		while (currentNode !=  startNode)
		{
			path.Add(currentNode);
			currentNode=  currentNode.parent;
		}
		path.Reverse();
		grid.path = path;
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
	    FindPath(player.position,target.position);
    }
}
