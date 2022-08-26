using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBasedVolume : MonoBehaviour
{
    public float maxDist = 2;
    public float maxVol = 1;
    AudioSource audioSource;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist > maxDist)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume =(1- dist / maxDist)*maxVol;
        }
    }
}
