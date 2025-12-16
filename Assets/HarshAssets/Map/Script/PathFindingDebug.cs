using System.Collections.Generic;
using NUnit.Framework;
using Pathfinding;
using UnityEngine;

public class PathFindingDebug : MonoBehaviour
{
    int[,] matriceDadjacente = new int[,]
    {
        {0,1,0,1,0,0,0,0 },
        {1,0,1,0,0,0,1,0 },
        {0,1,0,0,0,0,1,0 },
        {1,0,0,0,1,0,0,0 },
        {0,0,0,1,0,1,0,0 },
        {0,0,0,0,1,0,0,0 },
        {0,1,1,0,0,0,0,1 },
        {0,0,0,0,0,0,1,0 },
    };
    void Start()
    {
        Graph graphe = new Graph(matriceDadjacente);
        List<int> cheminDFS = graphe.DijkstraSearch(0, 7);
        foreach (int i in cheminDFS)
        {
            Debug.Log(i);
        }
    }

    void Update()
    {

    }
}
