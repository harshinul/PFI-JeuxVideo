using System;
using UnityEngine;

public class CreateTerrain : MonoBehaviour
{
    [SerializeField] GameObject defaultTile;
    [SerializeField] int mapWidth = 4;
    [SerializeField] int mapHeight = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HexTile[,] terrain = BuildTerrain();
        int[,] matrix = CreateMatrix();
    }

    private int[,] CreateMatrix()
    {
        int[,] matrix = new int[mapWidth * mapHeight, mapWidth * mapHeight];

        for (int row = 0; row < mapHeight; row++)
        {
            for (int column = 0; column < mapWidth; column++)
            {
                //index de la node active row + column * mapWidth
                if(column != 0)
                {
                    //en haut a gauche
                    if (column % 2 == 0)
                    {
                        matrix[column + row * mapWidth, column - 1 + row * mapWidth] = 1;
                    }
                    else
                    {
                        if(row != 0)
                        {
                            matrix[column + row * mapWidth, column + (row - 1) * mapWidth] = 1;
                        }
                    }

                    //en bas a gauche
                    if( column % 2 == 0)
                    {
                        if (row != mapHeight - 1)
                        {
                            matrix[column + row * mapWidth, column - 1 + (row - 1) * mapWidth] = 1;
                        }
                    }
                    else
                    {
                        matrix[column + row * mapWidth, (column - 1) + row * mapWidth] = 1;
                    }
                }
                
                //en haut
                if (row != 0)
                    matrix[column + row * mapWidth, column + (row - 1) * mapWidth] = 1;
                //en bas
                if(row != mapHeight - 1)   
                    matrix[column + row * mapWidth, column + (row + 1) * mapWidth] = 1;


                if(column != mapWidth - 1)
                {
                    //en haut a droite
                    if (column % 2 == 0)
                    {
                        matrix[column + row * mapWidth, column + 1 + row * mapWidth] = 1;
                    }
                    else
                    {
                        if(row != 0)
                        {
                            matrix[column + row * mapWidth, column - 1 + (row - 1) * mapWidth] = 1;                            
                        }                        
                    }

                    //en bas a droite
                    if(column %2 == 0)
                    {
                        if(row != mapHeight - 1)
                        {
                            matrix[column + row * mapWidth, column + 1 + row * mapWidth] = 1;
                        }
                    }
                    else
                    {
                        matrix[column + row * mapWidth, column + 1 + (row + 1) * mapWidth] = 1;
                    }                   
                }                
            }
        }
        return matrix;
    }

    //private class HexTile 
    //{
    //    [,] BuildTerrain()
    //    {
    //        HexTile[,] terrain = new HexTile[mapHeight, mapWidth];
    //        Vector3 basePosition = transform.position;

    //        for (int row = 0; row < mapWidth; ++row)
    //        {
    //            for (int column = 0; column < mapHeight; ++column)
    //            {
    //                GameObject tile = GameObject.Instantiate(defaultTile, new Vector3(basePosition.x + column, basePosition.y, basePosition.z + row - (column % 2 * 0.5f)), Quaternion.identity, transform);
    //                terrain[row, column] = tile.GetComponent<HexTile>();
    //            }
    //        }
    //        return terrain;
    //    }
    //}
    
}
