using System;
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

    public event Action<int> beatTrigger;

    public void BeatTrig(int beatCount)
    {
        if (beatTrigger != null)
        {
            beatTrigger(beatCount);
        }
    }
    
    

}
