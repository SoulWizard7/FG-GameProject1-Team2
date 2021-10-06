using UnityEngine;
public class Tile : MonoBehaviour
{
    public Vector2Int tilePos;
    public bool isObstacle;
    private SpriteRenderer _sprite;
    public bool hasEnemy;

    public EnemyBase currentEntity;
    
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        
        BeatEvents.instance.beatTrigger += ChangeColor;
    }

    private void ChangeColor(int beatCount)
    {
        _sprite.material.color = BeatEvents.instance.danceFloorColors[Random.Range(0, BeatEvents.instance.danceFloorColors.Count)];
    }
}
