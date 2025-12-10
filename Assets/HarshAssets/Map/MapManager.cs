using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static LocationComponent;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    [SerializeField] Transform playerTransform;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera mapCamera;

    [SerializeField] Canvas mapCanvas;
    [SerializeField] Canvas miniMapCanvas;

    public LocationComponent[] locations; //public for debug

    [SerializeField] List<LocationNode> matrix;
    GraphHarsh graph;
    [SerializeField] int startIndex;

    bool isMapActive;

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
        isMapActive = false;
        HideMap();
        locations = FindObjectsByType<LocationComponent>(FindObjectsSortMode.None);

        int[,] adjacencyMatrix = CreateMatrix();
        graph = new(adjacencyMatrix, matrix);
    }

    // Update is called once per frame
    void Update()
    {
        //CreateNavigation();
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

    public void test(Vector3 position)
    {
        ResetColor();

        matrix[FindClosestLositionOnMap(position)].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void ResetColor()
    {
        foreach (var tile in matrix)
        {
            tile.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    void CreateNavigation(int startIndex, int destinationIndex)
    {
        foreach (var i in graph.AStarSearch(startIndex, destinationIndex))
        {
            matrix[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public int FindClosestLositionOnMap(Vector3 position)
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity; 

        for (int i = 0; i < locations.Length; i++)
        {
            float distance = Vector3.Distance(position, locations[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = locations[i].index;
            }
        }

        return closestIndex;
    }

    public void SetDestination(Vector3 destinationPosition)
    {
        ResetColor();
        CreateNavigation(FindClosestLositionOnMap(playerTransform.position), FindClosestLositionOnMap(destinationPosition));
    }

    void ShowMap()
    {
        mainCamera.enabled = false;
        mapCamera.enabled = true;
        miniMapCanvas.enabled = false;
    }

    void HideMap()
    {
        mainCamera.enabled = true;
        mapCamera.enabled = false;
        miniMapCanvas.enabled = true;
    }

    public void InputShowMap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!isMapActive)
            {
                isMapActive = true;
                ShowMap();
            }
            else
            {
                isMapActive = false;
                HideMap();
            }
        }
    }
}
