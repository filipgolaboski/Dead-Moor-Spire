using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{

    public float damage;
    public float immunityTime;

    float currentImmunityTime = 0;

    private void Start()
    {
        currentImmunityTime = 0;
    }

    private void Update()
    {
        if(currentImmunityTime > 0)
        {
            currentImmunityTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && currentImmunityTime <= 0)
        {
            collision.gameObject.GetComponentInChildren<TorchMechanic>().DamageTorch(damage);
            currentImmunityTime = immunityTime;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player" && currentImmunityTime <= 0)
        {
            other.GetComponentInChildren<TorchMechanic>().DamageTorch(damage);
            currentImmunityTime = immunityTime;
        }
    }

}
