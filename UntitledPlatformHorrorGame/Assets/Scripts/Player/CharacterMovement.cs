using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    public float movSpeed = 3f;
    private float horizontal;
    public float jumpForce = 30f;
    public float jumpTime = 1f;
    private float currentJumpTime;

    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();

        rbPlayer.freezeRotation = true;

        currentJumpTime = 0f;

        isJumping = false;  
    }
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rbPlayer.velocity = new Vector2(horizontal * movSpeed, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            currentJumpTime = jumpTime;
        }

        if(currentJumpTime >= 0f)
        {
            rbPlayer.velocity += Vector2.up * Mathf.Lerp(jumpForce, 0, (jumpTime - currentJumpTime) / jumpTime);
            currentJumpTime -= Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    } 
}
