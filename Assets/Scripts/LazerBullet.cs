using System;
using System.Collections;
using UnityEngine;

public class LazerBullet : MoveableEntity
{
    // Had a problem to figure out what speed and lazer speed did in the inspector, wanted to try slow down lazer
    // but didn´t know what values to change.
    
    public int range = 10;
    [Range(0, 0.2f)] public float lazerSpeed = 0.1f;
    // The maximum range the center of the bullet needs to be from the center of an enemy for it to be considered a hit
    public float hitRadius = 0.5f;

    [NonSerialized]public Vector2Int direction = Vector2Int.right;
    
    void Start()
    {
        Move(direction * range, false);
        Destroy(gameObject, moveSpeed);
    }

    // Override coroutine to also check for damage to enemies while moving
    public override IEnumerator MoveToPos(Vector2Int pos)
    {
        Vector3 startPos = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector2.Lerp(startPos, pos, t);
            HandleCollisions();
            yield return 0;
            t += (Time.deltaTime / moveSpeed);
        }

        transform.position = (Vector2)pos;
        HandleCollisions();
        yield return null;
    }

    private void HandleCollisions()
    {
        foreach (EnemyBase enemy in EntityManager.instance.enemies)
        {
            float rangeToEntity = ((Vector2)enemy.transform.position - (Vector2)this.transform.position).magnitude;

            if (rangeToEntity <= hitRadius)
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);
                break;
            }
        }
    }
}
