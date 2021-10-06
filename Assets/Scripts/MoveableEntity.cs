using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEntity : MonoBehaviour
{
    public Vector2Int currentPos;
    public float moveSpeed = 0.05f;

    private Coroutine activeCoroutine;

    protected Vector2Int newPos;

    public Vector2Int entityLastPosition = Vector2Int.zero;

    public virtual void Move(Vector2Int direction, bool restrictToDanceFloor)
    {
        entityLastPosition = currentPos;
        
        newPos = currentPos + direction;
        
        if (!GridManager.instance.IsTileAvailible(newPos, restrictToDanceFloor))
            return;

        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        
        activeCoroutine = StartCoroutine(MoveToPos(newPos));

    }

    public IEnumerator MoveToPos(Vector2Int pos)
    {
        currentPos = pos;
        Vector2 targetPos = pos;

        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, t);
            yield return 0;
            t += Time.deltaTime;
        }

        transform.position = targetPos;
        yield return null;
    }
}
