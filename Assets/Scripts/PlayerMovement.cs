using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MoveableEntity
{
    private BeatManager _beatManager;
    private VisualBeatTimer _visualBeatTimer;

    public GameObject lazerPrefab;
    public GameObject aim;

    private int _healthIntAnimator = 0;
    
    private AudioSource _audioSource;
    public List<AudioClip> soundEffects;

    bool isDead = false;

    private Animator _animator;
    private static readonly int HealthBool = Animator.StringToHash("healthBool");
    private static readonly int PlayerDeath = Animator.StringToHash("PlayerDeath");
    private static readonly int PlayerWin = Animator.StringToHash("PlayerWin");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _beatManager = GameObject.Find("BeatManager").GetComponent<BeatManager>();
        _visualBeatTimer = GameObject.Find("VisualBeatTimer").GetComponent<VisualBeatTimer>();
    }

    private void Update()
    {
        if (_beatManager.hasWon)
        {
            _animator.SetBool(PlayerWin, true);
            return;
        }
        
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
                    AimCrosshair(inputMovement);
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

    void AimCrosshair(Vector2Int aimDir)
    {
        aim.transform.position = transform.position + new Vector3(aimDir.x, aimDir.y, transform.position.z);

        aim.transform.rotation =
            Quaternion.LookRotation(Vector3.forward, new Vector3(aimDir.x, aimDir.y, transform.position.z));
    }

    void ShootLazer()
    {
        LazerBullet lazer =  Instantiate(lazerPrefab, transform.position, Quaternion.identity).GetComponent<LazerBullet>();
        lazer.direction = facingDir;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        //_healthIntAnimator++;
        
        for (int i = 0; i < _visualBeatTimer.healthContainers.Count; i++)
        {
            _visualBeatTimer.healthContainers[i].GetComponent<Animator>().SetBool(HealthBool, health >= (i+1));
        }

        if (health <= 0)
        {
            if (!isDead)
            {
                _visualBeatTimer.deathTab.SetActive(true);
                isDead = true;
                _animator.SetBool(PlayerDeath, true);
                _audioSource.PlayOneShot(soundEffects[2]);
                GameObject.Find("EntityManager").GetComponent<EntityManager>().AllEnemiesStopMoving();
            }
            return;
        }
        _audioSource.PlayOneShot(soundEffects[Random.Range(0, 2)]);
    }

    public void GetHealth(int hp)
    {
        if (health == 3) return;

        health += hp;
        
        for (int i = 0; i < _visualBeatTimer.healthContainers.Count; i++)
        {
            _visualBeatTimer.healthContainers[i].GetComponent<Animator>().SetBool(HealthBool, health >= (i+1));
        }
    }
}
