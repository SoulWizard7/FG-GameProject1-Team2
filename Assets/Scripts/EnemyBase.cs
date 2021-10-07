using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MoveableEntity
{
    public EntityManager entityManager;
    public int beatsUntilMove;

    void Start()
    {
        BeatEvents.instance.beatTrigger += OnBeat;
    }

    protected virtual void OnBeat(int beatCount)
    {
        // This should probably be overridden by child classes but here is a basic functionality for moving towards player

        Vector2Int dirToPlayer = entityManager.GetPlayerPos() - GetRoundedPos();
        Vector2Int dirToPlayerClamped = new Vector2Int(Mathf.Clamp(dirToPlayer.x, -1, 1), Mathf.Clamp(dirToPlayer.y, -1, 1));
        if (Mathf.Abs(dirToPlayer.x) >= Mathf.Abs(dirToPlayer.y))
            dirToPlayerClamped.y = 0;
        else
            dirToPlayerClamped.x = 0;

        Move(dirToPlayerClamped, false);
    }
    
    // To anton: Was not able to refactor this into MovableEntity so I just made it into an override for some reason.
    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            BeatEvents.instance.beatTrigger -= OnBeat;
            EntityManager.instance.enemies.Remove(this);

            Destroy(gameObject, 0.1f);
        }
    }

    
}
