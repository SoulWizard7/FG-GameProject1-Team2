using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public GameObject tilePrefab;
    
    public Tile[,] tiles;

    void Awake()
    {
        instance = this;
        tiles = new Tile[gridSizeX, gridSizeY];
        SetupGrid();
    }

    void SetupGrid()
    {
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                GameObject go = Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity, gameObject.transform);

                go.name = "Tile (" + i + ", " + j +")";
                
                Tile tile = go.GetComponent<Tile>();
                tile.tilePos.x = i;
                tile.tilePos.y = j;

                tiles[i, j] = tile;
            }
        }
    }

    public bool IsTileAvailible(Vector2Int tilePos, bool limitToDanceFloor)
    {
        bool onDanceFloor = true;
        if (tilePos.x < 0 || tilePos.x > GridManager.instance.gridSizeX - 1 || tilePos.y < 0 || tilePos.y > GridManager.instance.gridSizeY - 1)
        {
            // Position outside of grid
            onDanceFloor = false;
            if (limitToDanceFloor)
            {
                return false;
            }
        }

        if (onDanceFloor)
        {
            if (tiles[tilePos.x, tilePos.y].isObstacle)
            {
                // Tile is obstacle
                return false;
            }
        }
        
        // TODO: Check for enemies occupying tile

        return true;
    }
}
