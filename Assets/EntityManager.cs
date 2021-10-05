using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager instance;

    private List<EnemyBase> enemies = new List<EnemyBase>();
    private PlayerMovement player;
    
    public GameObject playerPrefab;
    public Vector2Int playerStartPosition = new Vector2Int(4, 4);

    public GameObject zombiePrefab;

    // Number of tiles away from dance floor enemies spawn.
    public int enemySpawnOffset = 3;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnPlayer();
        BeatEvents.instance.beatTrigger += OnBeat;
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, new Vector2(playerStartPosition.x, playerStartPosition.y), Quaternion.identity).GetComponent<PlayerMovement>();
        player.currentPos = playerStartPosition;
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.player = player.transform;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Randomize what side to spawn on
        int side = Random.Range(0, 4);
        // Randomize what tile along that side to spawn on
        int spawnTile = Random.Range(0, GridManager.instance.gridSizeX);

        Vector2Int spawnPos = Vector2Int.zero;

        switch (side)
        {
            case 0: // Top
                spawnPos = new Vector2Int(spawnTile, -enemySpawnOffset);
                break;
            case 1: // Right
                spawnPos = new Vector2Int(GridManager.instance.gridSizeX + enemySpawnOffset, spawnTile);
                break;
            case 2: // Bottom
                spawnPos = new Vector2Int(spawnTile, GridManager.instance.gridSizeY + enemySpawnOffset);
                break;
            case 3: // Left
                spawnPos = new Vector2Int(-enemySpawnOffset, spawnTile);
                break;
        }

        EnemyBase enemy = Instantiate(enemyPrefab, (Vector2)spawnPos, Quaternion.identity).GetComponent<EnemyBase>();
        enemy.currentPos = spawnPos;
        enemy.entityManager = this;
        enemies.Add(enemy);
    }

    private void OnBeat(int beatCount)
    {
        if (beatCount % 8 == 0)
        {
            SpawnEnemy(zombiePrefab);
        }
    }

    public Vector2Int GetPlayerPos()
    {
        return player.currentPos;
    }
}
