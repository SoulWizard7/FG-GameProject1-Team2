using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public GameObject tilePrefab;
    public GameObject playerPrefab;
    
    public Tile[,] tiles;
    
    private PlayerMovement player;
    public Vector2Int playerStartPosition = new Vector2Int(5, 5);
    
    void Awake()
    {
        tiles = new Tile[gridSizeX, gridSizeY];
        SetupGrid();
        InsertPlayer();
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

    void InsertPlayer()
    {
        GameObject player = Instantiate(playerPrefab, new Vector2(playerStartPosition.x,playerStartPosition.y), Quaternion.identity);
        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm.currentPos = playerStartPosition;
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.player = player.transform;
    }
}
