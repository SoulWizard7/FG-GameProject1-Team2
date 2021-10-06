using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    private float _musicStart;
    private float _beatTime;
    public float bpm = 128;
    private float _bps;
    public float songStartOffset;
    public int currentBeat = 0;

    [Range(0, 0.20f)] public float offsetMilliSeconds;
    
    public bool startTheBeat = false;
    private bool _playSong = false;
    public bool playerCanInput;
    //public bool playerDidInputThisBeat;


    [NonSerialized]public AudioSource song;

    private void Start()
    {
        _bps = 60 / bpm;
        Debug.Log("bps = " + _bps);
        
        song = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.anyKey) { startTheBeat = true; }
        
        if (!startTheBeat)
        {
            _musicStart = Time.time - songStartOffset;
            return;
        }
        if (!_playSong && startTheBeat)
        {
            _playSong = true;
            song.Play();
        }
        
        Beat();
        CurrentBeatCounter();
        BeatWithInputOffset();
    }

    void Beat()
    {
        float time = Time.time - _musicStart;
        _beatTime = time / _bps;
    }
    
    void CurrentBeatCounter()
    {
        if (_beatTime > currentBeat)
        {
            currentBeat++;
            BeatEvents.instance.BeatTrig(currentBeat);
            //Debug.Log("current Beat counter : " + currentBeat);
        }
    }

    void BeatWithInputOffset()
    {
        if (_beatTime > currentBeat - offsetMilliSeconds || _beatTime < (currentBeat - 1 + offsetMilliSeconds))
        {
            playerCanInput = true;
        }
        else
        {
            playerCanInput = false;
        }
    }
}
