using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MoveableEntity
{
    private BeatManager _beatManager;

    public GameObject lazerPrefab;
    
    private void Start()
    {
        _beatManager = GameObject.Find("BeatManager").GetComponent<BeatManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            Vector2Int inputMovement = new Vector2Int(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));

            if (inputMovement.magnitude > 0)
            {
                // Player moved
                if (_beatManager.playerCanInput)
                {
                    Move(inputMovement, true);
                }
                else
                {
                    Debug.Log("Input NOT on beat!");
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_beatManager.playerCanInput)
            {
                Debug.Log("Input FIRE! was on beat!");
                ShootLazer();    
            }
            else
            {
                Debug.Log("Input FIRE! was NOT on beat!");
            }
        }
    }

    void ShootLazer()
    {
        Vector2Int directionToShoot = (entityLastPosition + (currentPos - entityLastPosition) * 2) - currentPos;
        
        //Debug.Log(directionToShoot);

        for (int i = 1; i < 10; i++)
        {
            Vector2Int nextTileToCheck = new Vector2Int(currentPos.x + (directionToShoot.x * i), currentPos.y + (directionToShoot.y * i));
            
            if (nextTileToCheck.x >= 0 && nextTileToCheck.x <= GridManager.instance.gridSizeX - 1 && 
                nextTileToCheck.y >= 0 && nextTileToCheck.y <= GridManager.instance.gridSizeY - 1)
            {
                Tile tile = GridManager.instance.tiles[nextTileToCheck.x, nextTileToCheck.y];
                
                if (tile.hasEnemy)
                {
                    LazerHitsEnemy(tile);
                    break;
                }
                else
                {
                    //Debug.Log("tiles have NO enemies");
                }
            }
            else
            {
                Vector2Int dir = new Vector2Int(directionToShoot.x * 20, directionToShoot.y * 20);
                LazerFiresWide(dir);
                break;
            }
        }
    }

    void LazerHitsEnemy(Tile tile)
    {
        GameObject lazer = Instantiate(lazerPrefab, transform.position, Quaternion.identity);
        LazerBullet lazerBullet = lazer.GetComponent<LazerBullet>();
        lazerBullet.target = new Vector2(tile.tilePos.x, tile.tilePos.y);
        
        tile.currentEntity.TakeDamage(1);

        Destroy(lazer, 0.1f);
    }

    void LazerFiresWide(Vector2Int direction)
    {
        GameObject lazer = Instantiate(lazerPrefab, transform.position, Quaternion.identity);
        LazerBullet lazerBullet = lazer.GetComponent<LazerBullet>();
        lazerBullet.target = new Vector2(direction.x + currentPos.x, direction.y + currentPos.y);
        
        Destroy(lazer, 0.5f);
    }
}
