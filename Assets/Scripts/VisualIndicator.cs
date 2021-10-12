using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualIndicator : MonoBehaviour
{
    [NonSerialized] public Transform _startSpot;
    [NonSerialized] public BeatManager _bm;
    [NonSerialized] public VisualBeatTimer _visualBeatTimer;
    
    private float _beatNr;
    private bool _onBeat;
    private bool _success;

    void Start()
    {
        BeatEvents.instance.succesfulInputOnBeat += InputOnBeat;
        _beatNr = _bm.currentBeat;
    }
    
    private void Update()
    {
        float t = _bm._beatTime - _beatNr;
        transform.position = Vector2.LerpUnclamped(_startSpot.position, _visualBeatTimer.transform.position, t/2);
    }
    
    void InputOnBeat(int something)
    {
        if (_onBeat)
        {
            //GetComponent<SpriteRenderer>().sprite = _visualBeatTimer.ringColors[0];
            GetComponent<Image>().color = _visualBeatTimer.ringColors[0]; //remove once we got the right sprites
            _success = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            _onBeat = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            _onBeat = false;
            if (!_success)
            {
                //GetComponent<SpriteRenderer>().sprite = _visualBeatTimer.indicatorSprites[2];
                GetComponent<Image>().color = _visualBeatTimer.ringColors[1]; //remove once we got the right sprites
            }
        }
    }
}
