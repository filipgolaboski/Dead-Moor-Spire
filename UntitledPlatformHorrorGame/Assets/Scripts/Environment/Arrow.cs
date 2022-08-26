using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ArrowHit: UnityEvent<RaycastHit2D> { }

public class Arrow : MonoBehaviour
{
    public float arrowForce;
    public bool fire;
    public LayerMask hitMask;
    public ArrowHit onArrowHit;
    public SingleSoundPlayer arrowFly;
    public SingleSoundPlayer arrowHit;

    bool flying;
    Rigidbody2D arrowBody;
    // Start is called before the first frame update
    void Start()
    {
        arrowBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fire)
        {
            arrowBody.constraints = RigidbodyConstraints2D.None;
            arrowBody.AddForce(transform.up * arrowForce, ForceMode2D.Impulse);
            flying = true;
            fire = false;
            arrowFly.PlaySingleClip();
        }

        if(flying)
        {
            RaycastHit2D hit =  Physics2D.Raycast(GetComponent<CapsuleCollider2D>().bounds.max, transform.up, .1f,hitMask);
            if (hit)
            {
                flying = false;
                arrowBody.constraints = RigidbodyConstraints2D.FreezeAll;
                onArrowHit.Invoke(hit);
                arrowHit.PlaySingleClip();
            }
        }

        if(arrowBody.velocity.sqrMagnitude < 0.1f && flying)
        {
            flying = false;
            arrowBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void Fire()
    {
        fire = true;
    }

}
