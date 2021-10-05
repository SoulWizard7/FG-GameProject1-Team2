using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [NonSerialized]public Transform player;
    [Range(0f, 1f)] public float smoothDamp;
    
    private Vector3 _currentVelocity;
    
    private void Update()
    {
        Vector3 camPos = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, camPos, ref _currentVelocity, smoothDamp);
    }
}
