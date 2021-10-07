using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{
    private static readonly int AnimChangeTrigger = Animator.StringToHash("AnimChangeTrigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnBeat(int beatCount)
    {
        
        if (beatCount % beatsUntilMove != 0)
            return;
        
        _animator.SetTrigger(AnimChangeTrigger);

        Vector2Int dirToPlayer = entityManager.GetPlayerPos() - GetRoundedPos();
        Vector2Int dirToPlayerClamped = new Vector2Int(Mathf.Clamp(dirToPlayer.x, -1, 1), Mathf.Clamp(dirToPlayer.y, -1, 1));
        if (Mathf.Abs(dirToPlayer.x) >= Mathf.Abs(dirToPlayer.y))
            dirToPlayerClamped.y = 0;
        else
            dirToPlayerClamped.x = 0;

        Move(dirToPlayerClamped, false);
    }
}
