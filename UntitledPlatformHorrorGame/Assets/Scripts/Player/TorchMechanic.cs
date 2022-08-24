using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TorchMechanic : MonoBehaviour
{
    // torch has an HP value of 100
    // the longer the game goes, the value goes down
    // the intensity and range of the torch goes lower and lower
    // dash lowers the intensity and range

    public GameObject torch1;
    public GameObject torch2;
    private float maxHP = 100f;
    private float torchCurrentHP;
    public float decayRate = 1000;

    float originalIntensity1;
    float originalIntensity2;
    float originalRadius1;
    float originalRadius2;

    void Start()
    {
        torchCurrentHP = maxHP;

        originalIntensity1 = torch1.GetComponent<Light2D>().intensity;
        originalIntensity2 = torch2.GetComponent<Light2D>().intensity;

        originalRadius1 = torch1.GetComponent<Light2D>().pointLightOuterRadius;
        originalRadius2 = torch2.GetComponent<Light2D>().pointLightOuterRadius;

    }

    void Update()
    {
        torchCurrentHP -= Time.deltaTime / decayRate;
        if (torchCurrentHP <= 0)
        {
            //die
        }

        torch1.GetComponent<Light2D>().intensity = originalIntensity1 * (torchCurrentHP / maxHP);
        torch2.GetComponent<Light2D>().intensity = originalIntensity2 * (torchCurrentHP / maxHP);

        torch1.GetComponent<Light2D>().pointLightOuterRadius = originalRadius1 * (torchCurrentHP / maxHP);
        torch2.GetComponent<Light2D>().pointLightOuterRadius = originalRadius2 * (torchCurrentHP/maxHP);
    }



}
