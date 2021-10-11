using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBomb : MoveableEntity
{
    public Transform sprite;
    public float explosionRadius = 0.5f;

    void Start()
    {
        Move(EntityManager.instance.GetPlayerPos() - new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), false);
    }

    // Override coroutine to also lower sprite to ground
    public override IEnumerator MoveToPos(Vector2Int pos)
    {
        Vector3 startPos = transform.position;
        float startHeight = sprite.localPosition.y;
        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector2.Lerp(startPos, pos, t);
            sprite.localPosition = new Vector3(sprite.localPosition.x, (1 - t) * startHeight, sprite.localPosition.z);
            yield return 0;
            t += (Time.deltaTime / moveSpeed);
        }

        transform.position = (Vector2)pos;
        sprite.localPosition = new Vector3(sprite.localPosition.x, 0, sprite.localPosition.z);
        Explode();

        yield return null;
    }

    private void Explode()
    {
        if((EntityManager.instance.GetPlayerPos() - (Vector2)transform.position).magnitude <= explosionRadius)
        {
            EntityManager.instance.DamagePlayer(1);
        }
        Destroy(gameObject, 0.5f);
    }
}
