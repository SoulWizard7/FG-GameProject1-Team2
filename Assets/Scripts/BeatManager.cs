using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public bool startTheBeat = false;
    private float musicStart;
    private float beatTime;
    public float bpm = 128;
    public int currentBeat = 0;

    private void Start()
    {
        float bps = 60 / bpm;
        Debug.Log("bps = " + bps);
    }

    private void Update()
    {
        if (!startTheBeat)
        {
            musicStart = Time.time;
            return;
        }
        
        TheBeat();
        
        CurrentBeatCounter();
    }

    void CurrentBeatCounter()
    {
        if (Mathf.Approximately(beatTime, currentBeat))
        {
            currentBeat++;
            Debug.Log(currentBeat);
        }
    }

    void TheBeat()
    {
         float time = Time.time - musicStart;
         float bps = 60 / bpm;
         beatTime = time / bps;
    }
    
}
