using UnityEngine;
public class Tile : MonoBehaviour
{
    public Vector2Int tilePos;
    public bool isObstacle;
    private SpriteRenderer _sprite;
    private BeatEvents _beatEvents;

    private void Start()
    {
        _beatEvents = GameObject.Find("BeatEvents").GetComponent<BeatEvents>();
        _sprite = GetComponent<SpriteRenderer>();
        
        BeatEvents.current.beatTrigger += ChangeColor;
    }

    private void ChangeColor()
    {
        _sprite.material.color = _beatEvents.danceFloorColors[Random.Range(0, _beatEvents.danceFloorColors.Count)];
    }
}
