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

    public override IEnumerator MoveToPos(Vector2Int pos)
    {
        Vector3 startPos = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector2.Lerp(startPos, pos, t);
            yield return 0;
            t += (Time.deltaTime / moveSpeed);
        }

        transform.position = (Vector2)pos;
        CheckDamagePlayer();
        yield return null;
    }

    private void CheckDamagePlayer()
    {
        if ((EntityManager.instance.GetPlayerPos() - (Vector2)transform.position).magnitude <= 0.5f)
        {
            EntityManager.instance.DamagePlayer(1);
        }
    }
}
