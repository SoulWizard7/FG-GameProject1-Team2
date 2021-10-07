using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMan : EnemyBase
{
    private static readonly int idleTrigger = Animator.StringToHash("PumpkinManIdle");
    private static readonly int danceTrigger = Animator.StringToHash("PumpkinManDance");
    private static readonly int attackTrigger = Animator.StringToHash("PumpkinManAttack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void OnBeat(int beatCount)
    {
        base.OnBeat(beatCount);

        int beatInSequence = (beatCount - firstBeat) % beatsUntilMove;

        switch (beatInSequence)
        {
            case 0:
                // Cycle anmation to dance since this is when we move
                _animator.ResetTrigger(attackTrigger);
                _animator.SetTrigger(idleTrigger);
                break;
            case 1:
                // Cycle animation to dance the beat after we moved
                _animator.ResetTrigger(idleTrigger);
                _animator.SetTrigger(danceTrigger);
                break;
            case 3:
                // Cycle animation to attack after dancing two beats
                _animator.ResetTrigger(danceTrigger);
                _animator.SetTrigger(attackTrigger);
                break;
        }
    }
}