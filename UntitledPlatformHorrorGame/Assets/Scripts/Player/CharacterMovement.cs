using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    private float movSpeed = 3f;
    private float horizontal;
    private float vertical;
    private float jumpForce = 10f;

    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();

        rbPlayer.freezeRotation = true;

        isJumping = false;  
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
       
    }

    void FixedUpdate()
    {
        if(horizontal > 0.1f  || horizontal < -0.1f)
        {
            rbPlayer.AddForce(new Vector2(horizontal * movSpeed, 0f), ForceMode2D.Impulse);
        }

        if(vertical > 0.1f)
        {
            rbPlayer.AddForce(new Vector2(0f, vertical * jumpForce), ForceMode2D.Impulse);
        }
    }
}
