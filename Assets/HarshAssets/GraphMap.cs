using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GraphMap : MonoBehaviour
{
    public static GraphMap Instance;
    [SerializeField] List<CubeNode> matrix;
    GraphHarsh graph;
    [SerializeField] int startIndex;
    //[SerializeField] int endIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
    void Start()
    {
        int[,] adjacencyMatrix = CreateMatrix();
        //graph = new(adjacencyMatrix, matrix);

        ResetColor();

        matrix[startIndex].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private int[,] CreateMatrix()
    {
        int[,] adjacencyMatrix = new int[matrix.Count, matrix.Count];

        for (int i = 0; i < matrix.Count; ++i)
        {
            foreach (IntTuple neighboor in matrix[i].neighboors)
            {
                adjacencyMatrix[i, neighboor.indexNeighboor] = neighboor.cost;
            }
        }

        return adjacencyMatrix;
    }

    public void SetDestinationTile(int endIndex)
    {
        ResetColor();

        foreach (var i in graph.AStarSearch(startIndex, endIndex))
        {
            matrix[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void ResetColor()
    {
        foreach (var tile in matrix)
        {
            tile.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}

public class GraphHarsh // seulement AStar et list de HexNode pour heuristic
{
    int[,] matriceAdjacence;
    List<LocationNode> matrix;
    public GraphHarsh(int[,] matriceAdjacence, List<LocationNode> matrix)
    {
        this.matriceAdjacence = matriceAdjacence;
        this.matrix = matrix;
    }

    public List<int> AStarSearch(int départ, int arrivé)
    {
        CustomQueue AExplorer = new CustomQueue();
        List<(int, float, int)> DéjaVu = new List<(int, float, int)>();

        AExplorer.Ajouter((départ, 0, -1));

        //////
        while (AExplorer.Count != 0)
        {
            (int index, float cout, int origine) actif = AExplorer.Retirer();
            DéjaVu.Add(actif);
            if (actif.index == arrivé)
            {
                break;
            }

            int lenght = matriceAdjacence.GetLength(0);
            for (int i = 0; i < lenght; ++i)
            {
                if (matriceAdjacence[actif.index, i] != 0)
                {
                    if (!Contient(DéjaVu, i))
                    {
                        AExplorer.Ajouter((i, matriceAdjacence[actif.index, i] + actif.cout + Heuristic(actif.index, arrivé), actif.index));
                    }
                }
            }
        }
        return invert(ConstruireChemin(DéjaVu, arrivé));

    }

    public float Heuristic(int index1, int index2)
    {
        return Vector3.Distance(matrix[index1].gameObject.transform.position, matrix[index1].gameObject.transform.position);
    }

    private List<int> ConstruireChemin(List<(int, float, int)> déjaVu, int arrivé)
    {
        List<int> chemin = new();
        foreach ((int node, float cout, int origine) i in déjaVu)
        {
            if (i.node == arrivé)
            {
                chemin.Add(i.node);
                chemin.AddRange(ConstruireChemin(déjaVu, i.origine));
            }
        }
        //exclure -1
        //inverse
        return chemin;
    }

    bool Contient(IEnumerable<(int, float, int)> list, int value)
    {
        foreach (var val in list)
        {
            if (val.Item1 == value)
                return true;
        }
        return false;
    }

    List<int> invert(List<int> a)
    {
        List<int> b = new List<int>(a.Count);
        for (int i = a.Count - 1; i >= 0; --i)
        {
            b.Add(a[i]);
        }
        return b;
    }
}
