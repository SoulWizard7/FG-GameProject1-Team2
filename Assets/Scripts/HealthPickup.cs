using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [NonSerialized] public PlayerMovement player;

    private void Start()
    {
        BeatEvents.instance.beatTrigger += OnBeat;
    }

    void OnBeat(int nr)
    {
        if (player.transform.position == transform.position)
        {
            player.GetHealth(1);
            BeatEvents.instance.beatTrigger -= OnBeat;
            Destroy(gameObject);
        }
    }
}
