using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBeatTimer : MonoBehaviour
{
    public List<Color> ringColors;
    public GameObject indicatorPrefab;
    private Transform _spawnPos;
    private BeatManager _bm;

    public List<GameObject> healthContainers;
    public GameObject deathTab;

    private Animator _animator;   
    private static readonly int BeatTrig = Animator.StringToHash("BeatTrig");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _bm = GameObject.Find("BeatManager").GetComponent<BeatManager>();
        _spawnPos = this.gameObject.transform.GetChild(0);
        BeatEvents.instance.beatTriggerEnvironment += OnBeat;
    }

    void OnBeat(int beatCount)
    {
        _animator.SetTrigger(BeatTrig);
        
        if (beatCount < 2) return;
        
        GameObject go = Instantiate(indicatorPrefab, _spawnPos.position, Quaternion.identity, transform);
        VisualIndicator v = go.GetComponent<VisualIndicator>();
        v._visualBeatTimer = this;
        v._startSpot = _spawnPos.transform;
        v._bm = _bm;
        Destroy(go, 5f);
    }
}
