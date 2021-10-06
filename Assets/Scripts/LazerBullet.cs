using System;
using UnityEngine;

public class LazerBullet : MonoBehaviour
{
    [NonSerialized]public Vector2 target;
    
    [Range(0, 0.2f)]public float lazerSpeed = 0.1f;
    private Vector2 currentVelocity;
    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, lazerSpeed);
    }
}
