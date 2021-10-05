using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2Int currentPos;

    private GridManager _gridManager;
    private BeatManager _beatManager;

    private void Start()
    {
        _gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        _beatManager = GameObject.Find("BeatManager").GetComponent<BeatManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if(!_beatManager.playerCanInput)
            {
                Debug.Log("Input NOT on beat!");
                return;
            }
            
            float h = Input.GetAxisRaw("Horizontal");
            int horizontal = Mathf.RoundToInt(h);

            if(GameObject.Find("Tile (" + (currentPos.x + horizontal) + ", " + (currentPos.y) +")") == null)
            { Debug.Log("tile DOES NOT exist"); return; }
            
            if(_gridManager.tiles[currentPos.x + horizontal, currentPos.y].isObstacle) { return; }
            
            //Debug.Log("tile exists");
            transform.position = _gridManager.tiles[currentPos.x + horizontal, currentPos.y].transform.position;
            currentPos = _gridManager.tiles[currentPos.x + horizontal, currentPos.y].tilePos;
        }
        

        if (Input.GetButtonDown("Vertical"))
        {
            if(!_beatManager.playerCanInput)
            {
                Debug.Log("Input NOT on beat!");
                return;
            }
            
            float v = Input.GetAxisRaw("Vertical");
            int vertical = Mathf.RoundToInt(v);

            if (GameObject.Find("Tile (" + (currentPos.x) + ", " + (currentPos.y + vertical) + ")") == null)
            { Debug.Log("tile DOES NOT exist"); return; }

            if (_gridManager.tiles[currentPos.x, currentPos.y + vertical].isObstacle) { return; }

            //Debug.Log("tile exists");
            transform.position = _gridManager.tiles[currentPos.x, currentPos.y + vertical].transform.position;
            currentPos = _gridManager.tiles[currentPos.x, currentPos.y + vertical].tilePos;
        }
    }
}
