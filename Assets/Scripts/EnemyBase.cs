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
}
