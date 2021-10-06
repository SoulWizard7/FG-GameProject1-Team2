using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MoveableEntity
{
    public EntityManager entityManager;
    public int health = 1;

    void Start()
    {
        BeatEvents.instance.beatTrigger += OnBeat;
    }

    protected virtual void OnBeat(int beatCount)
    {
        // This should probably be overridden by child classes but here is a basic functionality for moving towards player

        Vector2Int dirToPlayer = entityManager.GetPlayerPos() - this.currentPos;
        Vector2Int dirToPlayerClamped = new Vector2Int(Mathf.Clamp(dirToPlayer.x, -1, 1), Mathf.Clamp(dirToPlayer.y, -1, 1));
        if (Mathf.Abs(dirToPlayer.x) >= Mathf.Abs(dirToPlayer.y))
            dirToPlayerClamped.y = 0;
        else
            dirToPlayerClamped.x = 0;

        Move(dirToPlayerClamped, false);
    }

    public override void Move(Vector2Int direction, bool restrictToDanceFloor)
    {
        base.Move(direction, restrictToDanceFloor);
        
        UpdateTileInfo(newPos);
    }
    
    void UpdateTileInfo (Vector2Int pos)
    {
        if (pos.x >= 0 && pos.x <= GridManager.instance.gridSizeX - 1 && pos.y >= 0 && pos.y <= GridManager.instance.gridSizeY - 1)
        {
            //Debug.Log("Tile does Exist");
        }
        else
        {
            //Debug.Log("Tile does not Exist");
            return;
        }
        
        GridManager.instance.tiles[pos.x, pos.y].hasEnemy = true;
        GridManager.instance.tiles[pos.x, pos.y].currentEntity = this;
        
        if (entityLastPosition.x >= 0 && entityLastPosition.x <= GridManager.instance.gridSizeX - 1 && 
            entityLastPosition.y >= 0 && entityLastPosition.y <= GridManager.instance.gridSizeY - 1)
        {
            GridManager.instance.tiles[entityLastPosition.x, entityLastPosition.y].hasEnemy = false;
            GridManager.instance.tiles[entityLastPosition.x, entityLastPosition.y].currentEntity = null;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // TODO Destroy enemies without errors
            
            //GridManager.instance.tiles[entityLastPosition.x, entityLastPosition.y].currentEntity = null;
            //GridManager.instance.tiles[currentPos.x, currentPos.y].currentEntity = null;
            
            //Destroy(gameObject, 0.1f);
            transform.position = new Vector2(100, 100);
            currentPos = new Vector2Int(100, 100);
            entityLastPosition = new Vector2Int(100, 100);
        }
    }
}
