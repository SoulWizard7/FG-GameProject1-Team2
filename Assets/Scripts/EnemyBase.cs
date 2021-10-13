using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MoveableEntity
{
    [NonSerialized] public EntityManager entityManager;
    public int beatsUntilMove;
    // firstBeat is to allow enemies to offset the beat count to when they were spawned so they stay in the same part of the animation cycle
    protected int firstBeat = -1;

    public GameObject enemyDeathParticlePrefab;

    [SerializeField] protected Animator _animator;

    void Start()
    {
        BeatEvents.instance.beatTrigger += OnBeat;
    }

    protected virtual void OnBeat(int beatCount)
    {
        // Implemented basic move function here so enemies can call base.OnBeat() in their own overrides for basic movement if they want it.

        if (firstBeat == -1)
            firstBeat = beatCount;

        if ((beatCount - firstBeat) % beatsUntilMove == 0)
        {
            Vector2Int dirToPlayer = entityManager.GetPlayerPos() - GetRoundedPos();
            Vector2Int dirToPlayerClamped = new Vector2Int(Mathf.Clamp(dirToPlayer.x, -1, 1), Mathf.Clamp(dirToPlayer.y, -1, 1));
            if (Mathf.Abs(dirToPlayer.x) >= Mathf.Abs(dirToPlayer.y))
                dirToPlayerClamped.y = 0;
            else
                dirToPlayerClamped.x = 0;

            Move(dirToPlayerClamped, false);
        }
    }
    
    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            BeatEvents.instance.beatTrigger -= OnBeat;
            EntityManager.instance.enemies.Remove(this);

            GameObject particles = Instantiate(enemyDeathParticlePrefab, transform.position, Quaternion.identity);
            Destroy(particles, 2f);
            
            //_audioEffects.PlayZombieDeath();

            Destroy(gameObject, 0.1f);
        }
    }
}
