using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    //Storing index and position of node
    public int index;
    public Vector3 pos;

    //gCost = movement cost from this node to a neighbour
    //hCost = estimated cost between this node and the end goal node
    //fCost = gCost + fCost
    public float gCost;
    public float hCost;
    public float fCost;

    //The node in which this node came from
    public PathNode cameFromTheNode;

    public PathNode(int index, Vector3 pos)
    {
        this.index = index;
        this.pos = pos;
    }

    //Simple function that adds gCost to hCost
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
