using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool characterDeath;
    public bool characterJump;
    public bool characterFalling;
    public bool characterIdle;
    public bool characterDashing;
    public float characterSpeed;
    public float direction;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterDeath)
        {
            KillCharacter();
        }

        UpdateAnimatorBool("CharacterJump", characterJump);
        UpdateAnimatorBool("CharacterDash", characterDashing);

        if (characterSpeed > 0 && !characterJump && !characterFalling && !characterDashing)
        {
            UpdateAnimatorFloat("CharacterRun", characterSpeed);
        }

        FlipCharacter();
    }

    public void KillCharacter()
    {
        characterDeath = true;
    }

    public void FlipCharacter()
    {
        if(direction > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(direction < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void UpdateAnimatorBool(string animatorState, bool stateValue)
    {
        if (animator.GetBool(animatorState) != stateValue)
        {
            animator.SetBool(animatorState, stateValue);
        }
    }

    public void UpdateAnimatorFloat(string animatorState, float stateValue)
    {
        if(animator.GetFloat(animatorState) != stateValue)
        {
            animator.SetFloat(animatorState, stateValue);  
        }
    }



}
