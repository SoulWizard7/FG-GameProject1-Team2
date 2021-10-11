using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerMovement : MoveableEntity
{
    private BeatManager _beatManager;
    private VisualBeatTimer _visualBeatTimer;

    public GameObject lazerPrefab;

    private int _healthIntAnimator = 0;
    
    private AudioSource _audioSource;
    public List<AudioClip> soundEffects;

    bool isDead = false;
    private static readonly int HealthBool = Animator.StringToHash("healthBool");

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _beatManager = GameObject.Find("BeatManager").GetComponent<BeatManager>();
        _visualBeatTimer = GameObject.Find("VisualBeatTimer").GetComponent<VisualBeatTimer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            if (isDead) return;
            
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

        if (Input.GetButtonDown("Fire1") && !isDead)
        {
            if (_beatManager.playerCanInput)
            {
                Debug.Log("Input FIRE! was on beat!");
                ShootLazer();
                BeatEvents.instance.SuccesfulInput(health);
                _audioSource.PlayOneShot(soundEffects[3]);
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
        _healthIntAnimator++;
        
        for (int i = 0; i < _healthIntAnimator; i++)
        {
            _visualBeatTimer.healthContainers[i].GetComponent<Animator>().SetBool(HealthBool, true);
            if (_healthIntAnimator >= 3) _healthIntAnimator = 3;
        }

        if (health <= 0)
        {
            if (!isDead)
            {
                _visualBeatTimer.deathTab.SetActive(true);
                isDead = true;
                _audioSource.PlayOneShot(soundEffects[2]);
            }
            return;
        }
        
        _audioSource.PlayOneShot(soundEffects[Random.Range(0, 2)]);
        
    }
}
