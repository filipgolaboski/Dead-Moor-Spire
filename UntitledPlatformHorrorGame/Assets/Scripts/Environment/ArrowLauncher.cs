using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public float shootTimer;
    public float arrowRespawnTimer;

    public Arrow[] arrows;

    float currentShootTimer;
    float currentArrowRespawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentShootTimer = shootTimer;
        currentArrowRespawnTimer = arrowRespawnTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentShootTimer <= 0)
        {
            if (currentArrowRespawnTimer == arrowRespawnTimer)
            {
                FireAllArrows();
            }

            currentArrowRespawnTimer -= Time.fixedDeltaTime;
        }

        
        if(currentArrowRespawnTimer <= 0 && currentArrowRespawnTimer<=0)
        {
            currentShootTimer = shootTimer;
            currentArrowRespawnTimer = arrowRespawnTimer;
            RespawnAllArrows();
        }
        currentShootTimer -= Time.fixedDeltaTime;
    }

    public void FireAllArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].Fire();
        }

    }

    public void RespawnAllArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].transform.localPosition = Vector3.zero;
            arrows[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
            arrows[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            arrows[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
}
