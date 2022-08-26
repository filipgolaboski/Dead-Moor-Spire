using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask platformLayerMask;

    public float movSpeed = 7f;
    public float jumpForce = 14f;
    public float dashCooldown = 2f;
    public float dashTime = 0.2f;
    public float dashSpeed = 20f;
    public float airTime = 0.1f;
    public float groundDistance = 0.1f;
    public float coyoteTime = 0.1f;

    private float horizontal;
    private bool jump;
    private bool isGrounded;
    private bool dashAvailable = true;
    private bool isDashing = false;
    private float currentAirTime;

    public Character character;

    public int nrOfJumps = 2;
    private int currentNrOfJumps;
    private float direction;
    float coyote;

    void Start()
    {
        
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rbPlayer = GetComponent<Rigidbody2D>();

        coyote = coyoteTime;
        currentNrOfJumps = nrOfJumps;
        currentAirTime = 0;
    }
    void Update()
    {
        if (character.characterDeath)
        {
            rbPlayer.velocity = Vector3.zero;
            return;
        }

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");
        character.characterSpeed = Mathf.Abs(horizontal) * movSpeed;

        if(horizontal != 0) 
        { 
            direction = Mathf.Sign(horizontal);
            character.direction = direction;
        }


        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
            if (jump)
            {
                currentNrOfJumps--;
                currentAirTime = airTime;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            currentAirTime = 0;
            jump = false;
        }

        if (dashAvailable && Input.GetKeyDown(KeyCode.LeftShift))
        {
            character.characterDashing = true;
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rbPlayer.velocity = new Vector2(horizontal * movSpeed, rbPlayer.velocity.y);
        isGrounded = IsGrounded();

        if (currentAirTime>0 && ((isGrounded || coyote > 0) || currentNrOfJumps > 0)) 
        {
            character.characterJump = true;
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, Mathf.Lerp(jumpForce, 0, currentAirTime/airTime));
            jump = false;
            currentAirTime -= Time.fixedDeltaTime;
        }
        else
        {
            character.characterJump = false;
        }

        if (isGrounded || coyote > 0)
        {
            currentNrOfJumps = nrOfJumps;
            if (isGrounded)
            {
                coyote = coyoteTime;
            }
            else
            {
                coyote -= Time.fixedDeltaTime;
            }
        }
        

        
    }

    public bool IsGrounded()
    {
        Vector2 right = new Vector2(capsuleCollider.bounds.center.x + capsuleCollider.bounds.size.x / 2, capsuleCollider.bounds.min.y);
        Vector2 left = new Vector2(capsuleCollider.bounds.center.x - capsuleCollider.bounds.size.x / 2, capsuleCollider.bounds.min.y);
        RaycastHit2D raycastHitright = Physics2D.Raycast(right, Vector2.down, groundDistance, platformLayerMask);
        RaycastHit2D raycastHitleft = Physics2D.Raycast(left, Vector2.down, groundDistance, platformLayerMask);
        Debug.DrawRay(right, Vector2.down*groundDistance, Color.green);
        Debug.DrawRay(left, Vector2.down*groundDistance, Color.green);
        return raycastHitleft.collider != null || raycastHitright.collider != null;
    }


    private IEnumerator Dash()
    {
        dashAvailable = false;
        isDashing = true;
        float gravity = rbPlayer.gravityScale;
        rbPlayer.gravityScale = 0f;
        rbPlayer.velocity = Vector2.zero;
        rbPlayer.AddForce(new Vector2(dashSpeed * direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        rbPlayer.gravityScale = gravity;
        isDashing = false;
        character.characterDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashAvailable = true;
        
    }
}
