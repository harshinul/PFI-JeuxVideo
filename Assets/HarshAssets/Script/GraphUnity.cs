using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GraphUnity : MonoBehaviour
{

    [SerializeField] List<CubeNode> matrix;
    Graph graph;
    [SerializeField] int startIndex;
    [SerializeField] int endIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[,] adjacencyMatrix = CreateMatrix();
        graph = new(adjacencyMatrix);

        foreach(var i in graph.DijkstraSearch(startIndex, endIndex))
        {
            matrix[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private int[,] CreateMatrix()
    {
        int[,] adjacencyMatrix = new int[matrix.Count,matrix.Count];

        for(int i = 0; i < matrix.Count; ++i)
        {
            foreach(IntTuple neighboor in matrix[i].neighboors)
            {
                adjacencyMatrix[i, neighboor.indexNeighboor] = neighboor.cost;
            }
        }

        return adjacencyMatrix;
    }

    public float Heuristic(int index1, int  index2)
    {
        return Vector3.Distance(matrix[index1].gameObject.transform.position, matrix[index1].gameObject.transform.position); 
    }
}

[Serializable]
public class CubeNode
{
    public GameObject gameObject;
    public List<IntTuple> neighboors;
}

[Serializable]
public struct IntTuple
{
    public int indexNeighboor;
    public int cost;
}
