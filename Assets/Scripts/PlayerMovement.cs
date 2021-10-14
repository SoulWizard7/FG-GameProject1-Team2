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
    
    private AudioSource _audioSource;
    public List<AudioClip> playerSoundEffects;

    bool isDead = false;

    private Animator _animator;
    private static readonly int HealthBool = Animator.StringToHash("healthBool");
    private static readonly int PlayerDeath = Animator.StringToHash("PlayerDeath");
    private static readonly int PlayerWin = Animator.StringToHash("PlayerWin");
    private static readonly int SpamBool = Animator.StringToHash("SpamBool");

    public int _spamInputs = -1;
    public float spamPenaltyTime = 1f;
    private bool _didSpam = false;

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

        if (_didSpam)
        {
            return;
        }

        if (!_beatManager._playerCanInput)
        {
            if (Input.anyKeyDown) _spamInputs++;
            if (_spamInputs >= 2)
            {
                Debug.Log("SPAM!");
                StartCoroutine(SpamDance());
            }
        }
        
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            if (isDead) return;
            
            Vector2Int inputMovement = new Vector2Int(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));

            if (inputMovement.magnitude > 0)
            {
                // Player moved
                if (_beatManager._playerCanInput)
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
            if (_beatManager._playerCanInput)
            {
                Debug.Log("Input FIRE! was on beat!");
                ShootLazer();
                BeatEvents.instance.SuccesfulInput(health);
                _audioSource.PlayOneShot(playerSoundEffects[3]);
            }
            else
            {
                Debug.Log("Input FIRE! was NOT on beat!");
            }
        }
    }

    IEnumerator SpamDance()
    {
        _didSpam = true;
        _animator.SetBool(SpamBool, true);
        yield return new WaitForSeconds(spamPenaltyTime);
        _animator.SetBool(SpamBool, false);
        _didSpam = false;
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
                _audioSource.PlayOneShot(playerSoundEffects[2]);
                GameObject.Find("EntityManager").GetComponent<EntityManager>().AllEnemiesStopMoving();
            }
            return;
        }
        _audioSource.PlayOneShot(playerSoundEffects[Random.Range(0, 2)]);
    }

    public void GetHealth(int hp)
    {
        _audioSource.PlayOneShot(playerSoundEffects[4]);
        if (health == 3) return;

        health += hp;
        
        for (int i = 0; i < _visualBeatTimer.healthContainers.Count; i++)
        {
            _visualBeatTimer.healthContainers[i].GetComponent<Animator>().SetBool(HealthBool, health >= (i+1));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        string tag = collision.gameObject.tag;

        if (tag == "Enemy")
        {
            TakeDamage(1);
        }
        else if (tag == "Health")
        {
            if (health != 3)
            {
                GetHealth(1);
                Destroy(collision.gameObject);
            }
        }
    }
}
