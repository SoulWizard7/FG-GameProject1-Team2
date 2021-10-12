using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{
    private static readonly int AnimChangeTrigger = Animator.StringToHash("AnimChangeTrigger");

    protected override void OnBeat(int beatCount)
    {
        base.OnBeat(beatCount);
        
        // Change animation when moving and then change back the beat after moving
        if((beatCount - firstBeat) % beatsUntilMove == 0 || beatCount % beatsUntilMove == 1)
            _animator.SetTrigger(AnimChangeTrigger);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            _audioEffects.PlayZombieDeath();
        }
    }
}
