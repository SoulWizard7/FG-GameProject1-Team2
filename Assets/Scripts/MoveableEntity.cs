using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEntity : MonoBehaviour
{
    public float moveSpeed = 0.05f;

    protected Coroutine activeCoroutine;

    protected Vector2Int facingDir = Vector2Int.left;
    
    public int health = 1;

    private bool _isFacingRight;

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

        if (direction.x > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
            Flip();
        }
        
        if (direction.x < 0 && _isFacingRight)
        {
            _isFacingRight = false;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public virtual IEnumerator MoveToPos(Vector2Int pos)
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
        yield return null;
    }

    // Gets transform position rounded to closest int
    public Vector2Int GetRoundedPos()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }


    public virtual void TakeDamage(int damage) { }
}
