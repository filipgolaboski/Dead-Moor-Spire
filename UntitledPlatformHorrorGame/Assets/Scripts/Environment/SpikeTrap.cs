using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GroundSpike[] groundSpikes;

    public bool startState;
    public bool timed = true;
    public float interval;
    public float activeTime;

    public bool sensory = false;
    public LayerMask sensorMask;
    public float sensorDelay;

    public SingleSoundPlayer openTrapPlayer;
    public SingleSoundPlayer closeTrapPlayer;

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
        if (timed)
        {
            if (currentInterval <= 0)
            {
                if (currentActiveTime <= 0)
                {
                    RiseAllSpikes();
                }
                currentActiveTime += Time.deltaTime;
            }

            if (currentActiveTime >= activeTime)
            {
                currentInterval = interval;
                currentActiveTime = 0;
                FallAllSpikes();
            }
            currentInterval -= Time.deltaTime;
        }
    }

    void RiseAllSpikes()
    {
        closeTrapPlayer.StopAudioSource();
        openTrapPlayer.PlaySingleClip();
        for (int i=0;i<groundSpikes.Length;i++)
        {
            groundSpikes[i].RiseSpikes();
        }
    }

    void FallAllSpikes()
    {
        openTrapPlayer.StopAudioSource();
        closeTrapPlayer.PlaySingleClip();
        for (int i = 0; i < groundSpikes.Length; i++)
        {
            groundSpikes[i].FallSpikes();
        }
    }

    IEnumerator RiseFallRoutine()
    {
        yield return new WaitForSeconds(sensorDelay);
        RiseAllSpikes();
        yield return new WaitForSeconds(activeTime);
        FallAllSpikes();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(sensory && sensorMask.value == (sensorMask | (1 << collision.gameObject.layer))){
            StartCoroutine(RiseFallRoutine());
        }        
    }

}
