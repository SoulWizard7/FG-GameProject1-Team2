using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMan : EnemyBase
{
    private static readonly int animState = Animator.StringToHash("AnimState");

    private enum AnimState
    {
        Idle = 0,
        Dance = 1,
        Attack = 2
    }

    public GameObject pumpkinBombPrefab;

    protected override void OnBeat(int beatCount)
    {
        base.OnBeat(beatCount);

        int beatInSequence = (beatCount - firstBeat) % beatsUntilMove;

        switch (beatInSequence)
        {
            case 0:
                _animator.SetInteger(animState, (int)AnimState.Dance);
                if (beatCount - firstBeat != 0)
                {
                    // Throw a bomb at end of throwing animation as long as this is not the first beat the enemy is spawned.
                    Instantiate(pumpkinBombPrefab, transform.position, Quaternion.identity);
                }
                break;
            case 4:
                // Cycle animation to attack after dancing four beats
                _animator.SetInteger(animState, (int)AnimState.Attack);
                break;
        }
    }
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            _audioEffects.PlayPumpkinDeath();
        }
    }
    
}