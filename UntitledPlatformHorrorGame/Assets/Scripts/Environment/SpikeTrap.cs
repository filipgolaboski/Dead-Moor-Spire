using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GroundSpike[] groundSpikes;

    public bool startState;
    public float interval;
    public float activeTime;

    float currentInterval;
    float currentActiveTime;

    // Start is called before the first frame update
    void Start()
    {
        if (startState)
        {
            currentInterval = 0;
            currentActiveTime = 0;
        }
        else
        {
            currentInterval = interval;
            currentActiveTime = 0;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInterval<=0)
        {
            if(currentActiveTime <= 0)
            {
                RiseAllSpikes();
            }
            currentActiveTime += Time.deltaTime;
        }

        if(currentActiveTime >= activeTime)
        {
            currentInterval = interval;
            currentActiveTime = 0;
            FallAllSpikes();
        }
        currentInterval -= Time.deltaTime;
    }

    void RiseAllSpikes()
    {
        for(int i=0;i<groundSpikes.Length;i++)
        {
            groundSpikes[i].RiseSpikes();
        }
    }

    void FallAllSpikes()
    {
        for (int i = 0; i < groundSpikes.Length; i++)
        {
            groundSpikes[i].FallSpikes();
        }
    }
}
