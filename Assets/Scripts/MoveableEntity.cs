using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEntity : MonoBehaviour
{
    public float moveSpeed = 0.05f;

    protected Coroutine activeCoroutine;

    protected Vector2Int facingDir;

    public virtual void Move(Vector2Int direction, bool restrictToDanceFloor)
    {
        facingDir = new Vector2Int(direction.x, direction.y); // Copy x and y variables separately to avoid creating a reference
        facingDir.Clamp(new Vector2Int(-1, -1), new Vector2Int(1, 1));

        Vector2Int newPos = GetRoundedPos() + direction;
        
        if (!GridManager.instance.IsTileAvailible(newPos, restrictToDanceFloor))
            return;

        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        
        activeCoroutine = StartCoroutine(MoveToPos(newPos));

    }

    public virtual IEnumerator MoveToPos(Vector2Int pos)
    {
        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, pos, t);
            yield return 0;
            t += Time.deltaTime;
        }

        transform.position = (Vector2)pos;
        yield return null;
    }

    // Gets transform position rounded to closest int
    public Vector2Int GetRoundedPos()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
}
