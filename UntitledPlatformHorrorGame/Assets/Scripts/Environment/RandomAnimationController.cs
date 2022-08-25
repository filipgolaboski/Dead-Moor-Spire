using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationController : MonoBehaviour
{
    public float randomChance;
    public float distanceFromCharacter;
    public Animator animator;
    public string animatorStateToPlay;

    GameObject playerObject;
    bool animationShown;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if(playerObject != null)
        {
            if (Vector2.Distance(transform.position, playerObject.transform.position) > distanceFromCharacter)
            {
                if (!animationShown)
                {
                    if (Random.Range(0f, 1f) < randomChance)
                    {
                        animationShown = true;
                        animator.SetBool(animatorStateToPlay, true);
                    }
                }

            }
            else
            {
                animator.SetBool(animatorStateToPlay, false);
                animationShown = false;
            }
        } 
    }

    public void TurnOffAnimationOnEnd()
    {
        animator.SetBool(animatorStateToPlay, false);
        animationShown = false;
    }

}
