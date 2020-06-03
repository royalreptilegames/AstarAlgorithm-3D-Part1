using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: This code in its given state is not optimal and will take a long time to run, see future tutorials for more efficient implementation

public class NodeNetwork : MonoBehaviour
{
    //MeshCollider that we will be using to base our pathnode positions on
    public MeshCollider myMeshCollider;
    private Mesh myMesh;

    //Stores the index for each pathnode generated in the network (Key is equal to the given nodes corresponding triangle in myMesh)
    public static Dictionary<int, PathNode> pathNodes = new Dictionary<int, PathNode>();
    //Stores an array of integers that are pointers towards a given nodes neighbouring nodes 
    public static Dictionary<int, List<int>> neighbourNodes = new Dictionary<int, List<int>>();

    void Start()
    {
        myMesh = myMeshCollider.sharedMesh;
        PopulatePathNodes();
        PopulateAllNeighbourNodes();
    }

    //Responsible for looping through all triangles in the given mesh and populating the pathNodes dictionary
    private void PopulatePathNodes()
    {
        //Loop through all triangles in myMesh
        for (int i = 0; i < myMesh.triangles.Length; i+= 3)
        {
            //Store vertex positions of current triangle
            Vector3 vert1 = myMesh.vertices[myMesh.triangles[i]];
            Vector3 vert2 = myMesh.vertices[myMesh.triangles[i + 1]];
            Vector3 vert3 = myMesh.vertices[myMesh.triangles[i + 2]];

            //Calculate centre point of triangle
            Vector3 centrePoint = (vert1 + vert2 + vert3) / 3;

            //Create and add node
            PathNode newNode = new PathNode(i, centrePoint);
            pathNodes.Add(i, newNode);
        }
    }

    //Responsible for looping through all pathnodes and populating the neighbourNodes dictionary accordingly
    private void PopulateAllNeighbourNodes()
    {
        //neighbourNodes.Add(0, PopulateNeighbourNodes(0));

        foreach(int i in pathNodes.Keys)
        {
            neighbourNodes.Add(i, PopulateNeighbourNodes(i));
        }
    }

    //Responsible for returning a list of neighbouring pathnodes given a specific node index
    private List<int> PopulateNeighbourNodes(int currentNode)
    {
        List<int> resultNodes = new List<int>();

        //store vertices of given triangle
        Vector3 currentVert1 = myMesh.vertices[myMesh.triangles[currentNode]];
        Vector3 currentVert2 = myMesh.vertices[myMesh.triangles[currentNode + 1]];
        Vector3 currentVert3 = myMesh.vertices[myMesh.triangles[currentNode + 2]];

        for (int i = 0; i < myMesh.triangles.Length; i+= 3)
        {
            //Store values for triangles we're comparing against
            Vector3 vert1 = myMesh.vertices[myMesh.triangles[i]];
            Vector3 vert2 = myMesh.vertices[myMesh.triangles[i + 1]];
            Vector3 vert3 = myMesh.vertices[myMesh.triangles[i + 2]];

            //Check to make sure we're not operating on the same node that we are looking at (making sure a node isn't a neighbour to itself)
            if(i != currentNode)
            {
                //If any of the triangles we're looping through share a vertex with our given triangle, add it to the list
                if(currentVert1 == vert1 || currentVert1 == vert2 || currentVert1 == vert3)
                {
                    if (pathNodes.ContainsKey(i))
                    {
                        resultNodes.Add(i);
                    }
                }else if(currentVert2 == vert1 || currentVert2 == vert2 || currentVert2 == vert3)
                {
                    if (pathNodes.ContainsKey(i))
                    {
                        resultNodes.Add(i);
                    }
                }else if(currentVert3 == vert1 || currentVert3 == vert2 || currentVert3 == vert3)
                {
                    if (pathNodes.ContainsKey(i))
                    {
                        resultNodes.Add(i);
                    }
                }
            }
        }

        return resultNodes;

    }

    //Drawing gizmos to give visual feedback
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        /*
        if(pathNodes != null && pathNodes.Count > 0)
        {
            foreach(PathNode node in pathNodes.Values)
            {
                Gizmos.DrawSphere(node.pos, 0.01f);
            }
        }
        */

        if(neighbourNodes != null && neighbourNodes.Count > 0)
        {
            foreach(List<int> i in neighbourNodes.Values)
            {
                foreach(int j in i)
                {
                    Gizmos.DrawSphere(pathNodes[j].pos, 0.01f);
                }
            }
        }
    }


}
