using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
	//	Open List to Store the Nodes That Is Not evaluated yet
	List<Node> Open = new List<Node>();
	//Closed List to Store the Nodes That has been evaluated
	List<Node> closed = new List<Node>();
	
	float FCost;
//Loop
//Current Node in open with the lowest f cost
//Remove current from Open
//	Add current to closed
//	If(current is the end node)
//	Return path found
//Foreach neighbor of the current node 
//If neighbor is not traversable or  neighbor is in closed 
//Skip to the next neighbor
//	If new path to neighbor is shorter or neighbor is not in open 
//Set f cost of neighbor 
//Set parent of neighbor to current 
//If neighbor is not in open 
//Add neighbor to open

    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
