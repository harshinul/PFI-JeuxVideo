using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavigateMapComponent))]
public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [SerializeField] Transform playerTransform;

    // Cameras
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera mapCamera;

    // Canvases
    [SerializeField] Canvas mapCanvas;
    [SerializeField] Canvas miniMapCanvas;

    //Line Renderer
    [SerializeField] LineRenderer navigationLine;

    //Components
    //LocationComponent[] locations;
    [SerializeField] List<LocationNode> matrix;
    GraphHarsh graph;
    NavigateMapComponent navigateMapComponent;

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

        // Get Components
        navigateMapComponent = GetComponent<NavigateMapComponent>();

    }
    void Start()
    {
        isMapActive = false;
        HideMap();
        disableLocationVisual();
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
        //ResetColor();

        matrix[FindClosestLositionOnMap(position)].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void disableLocationVisual()
    {
        foreach (var tile in matrix)
        {
            tile.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void CreateNavigation(int startIndex, int destinationIndex)
    {
        List<int> path = new List<int>();
        path = graph.AStarSearch(startIndex, destinationIndex); // on crée le path
        navigationLine.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            navigationLine.SetPosition(i, matrix[path[i]].gameObject.transform.position + Vector3.up * 0.1f); // petit décalage en Y
            matrix[path[i]].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }


    public int FindClosestLositionOnMap(Vector3 position)
    {
        int closestIndex = -1;
        float closestDistance = Mathf.Infinity; 

        for (int i = 0; i < matrix.Count; i++)
        {
            float distance = Vector3.Distance(position, matrix[i].gameObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = matrix[i].index;
            }
        }

        return closestIndex;
    }

    public void SetDestination(Vector3 destinationPosition)
    {
        //ResetColor();
        CreateNavigation(FindClosestLositionOnMap(playerTransform.position), FindClosestLositionOnMap(destinationPosition));
    }


    void ShowMap()
    {
        mainCamera.enabled = false;
        mapCamera.enabled = true;
        miniMapCanvas.enabled = false;
        navigateMapComponent.EnableNavigation();
    }

    void HideMap()
    {
        mainCamera.enabled = true;
        mapCamera.enabled = false;
        miniMapCanvas.enabled = true;
        navigateMapComponent.DisableNavigation();
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
[Serializable]
public class LocationNode
{
    public int index;
    public GameObject gameObject;
    public List<IntTuple> neighboors;
}
