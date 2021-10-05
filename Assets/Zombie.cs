using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{
    protected override void OnBeat(int beatCount)
    {
        if (beatCount % 2 != 0)
            return;

        Vector2Int dirToPlayer = entityManager.GetPlayerPos() - this.currentPos;
        Vector2Int dirToPlayerClamped = new Vector2Int(Mathf.Clamp(dirToPlayer.x, -1, 1), Mathf.Clamp(dirToPlayer.y, -1, 1));
        if (Mathf.Abs(dirToPlayer.x) >= Mathf.Abs(dirToPlayer.y))
            dirToPlayerClamped.y = 0;
        else
            dirToPlayerClamped.x = 0;

        Move(dirToPlayerClamped, false);
    }
}
