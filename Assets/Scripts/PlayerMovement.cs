using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MoveableEntity
{
    private BeatManager _beatManager;

    private void Start()
    {
        _beatManager = GameObject.Find("BeatManager").GetComponent<BeatManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            Vector2Int inputMovement = new Vector2Int(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));

            if (inputMovement.magnitude > 0)
            {
                // Player moved
                if (_beatManager.playerCanInput)
                {
                    Move(inputMovement);
                }
                else
                {
                    Debug.Log("Input NOT on beat!");
                }
            }
        }
    }
}
