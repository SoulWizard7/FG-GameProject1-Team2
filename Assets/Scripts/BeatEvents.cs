using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEvents : MonoBehaviour
{
    public List<Color> danceFloorColors;
    
    public static BeatEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action beatTrigger;

    public void BeatTrig()
    {
        if (beatTrigger != null)
        {
            beatTrigger();
        }
    }

}
