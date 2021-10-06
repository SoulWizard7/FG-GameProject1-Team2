using System;
using System.Collections;
using UnityEngine;

public class LazerBullet : MoveableEntity
{
    public int range = 10;
    [Range(0, 0.2f)] public float lazerSpeed = 0.1f;
    // The maximum range the center of the bullet needs to be from the center of an enemy for it to be considered a hit
    public float hitRadius = 0.5f;

    [NonSerialized]public Vector2Int direction = Vector2Int.right;
    
    void Start()
    {
        Move(direction * range, false);
        Destroy(gameObject, lazerSpeed);
    }

    // Override coroutine to also check for damage to enemies while moving
    public override IEnumerator MoveToPos(Vector2Int pos)
    {
        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, pos, t);
            HandleCollisions();
            yield return 0;
            t += Time.deltaTime;
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
