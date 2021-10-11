using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MoveableEntity
{
    private BeatManager _beatManager;

    public GameObject lazerPrefab;
    
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
                    Move(inputMovement, true);
                    BeatEvents.instance.SuccesfulInput(health);
                }
                else
                {
                    Debug.Log("Input NOT on beat!");
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_beatManager.playerCanInput)
            {
                Debug.Log("Input FIRE! was on beat!");
                ShootLazer();
                BeatEvents.instance.SuccesfulInput(health);
            }
            else
            {
                Debug.Log("Input FIRE! was NOT on beat!");
            }
        }
    }

    void ShootLazer()
    {
        LazerBullet lazer =  Instantiate(lazerPrefab, transform.position, Quaternion.identity).GetComponent<LazerBullet>();
        lazer.direction = facingDir;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // TODO: implement "lose" functionalty here. Temporarily just going back to menu.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
