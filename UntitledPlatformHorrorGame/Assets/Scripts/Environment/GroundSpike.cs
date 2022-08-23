using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundSpike : MonoBehaviour
{
    public float riseSpeed;
    public float fallSpeed;

    public Vector2 risePosition;
    public Vector2 fallPosition;

    public SpriteRenderer spikeBase;
    public SpriteRenderer spikeRenderer;

    public UnityEvent onRiseStart;
    public UnityEvent onRiseEnd;

    public UnityEvent onFallStart;
    public UnityEvent onFallEnd;

    [HideInInspector]
    public bool rising;
    [HideInInspector]
    public bool falling;

    public bool riseTest;
    public bool fallTest;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rising && !falling)
        {
            spikeBase.gameObject.SetActive(true);
            spikeRenderer.transform.localPosition = Vector3.Lerp(spikeRenderer.transform.localPosition, risePosition, riseSpeed * Time.deltaTime);
            if(Vector3.Distance(spikeRenderer.transform.localPosition, risePosition) < 0.1f)
            {
                rising = false;
                onRiseEnd.Invoke();
            }
        }

        if (falling && !rising)
        {
            spikeRenderer.transform.localPosition = Vector3.Lerp(spikeRenderer.transform.localPosition, fallPosition, fallSpeed * Time.deltaTime);
            if (Vector3.Distance(spikeRenderer.transform.localPosition, fallPosition) < 0.1f)
            {
                falling = false;
                spikeBase.gameObject.SetActive(false);
                onFallEnd.Invoke();
            }
        }

#if UNITY_EDITOR
        if (riseTest)
        {
            riseTest = false;
            RiseSpikes();
        }

        if (fallTest)
        {
            fallTest = false;
            FallSpikes();
        }
#endif
    }

    public void RiseSpikes()
    {
        if (!falling)
        {
            rising = true;
        }

        onRiseStart.Invoke();
    }

    public void FallSpikes()
    {
        if (!rising)
        {
            falling = true;
        }

        onFallStart.Invoke();
    }

    public void Stop()
    {
        rising = falling = false;
    }
}
