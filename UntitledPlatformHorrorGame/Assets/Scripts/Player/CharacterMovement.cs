using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask platformLayerMask;

    public float movSpeed = 7f;
    public float jumpForce = 14f;
    public float dashCooldown = 2f;
    public float dashTime = 0.2f;
    public float dashSpeed = 20f;
    public float airTime = 0.1f;
    public float groundDistance = 0.1f;

    private float horizontal;
    private bool jump;
    private bool isGrounded;
    private bool dashAvailable = true;
    private bool isDashing = false;
    private float currentAirTime;

    public int nrOfJumps = 2;
    private int currentNrOfJumps;

    void Start()
    {
        
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rbPlayer = GetComponent<Rigidbody2D>();

        currentNrOfJumps = nrOfJumps;
        currentAirTime = 0;
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");


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
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rbPlayer.velocity = new Vector2(horizontal * movSpeed, rbPlayer.velocity.y);
        isGrounded = IsGrounded();

        if (currentAirTime>0 && (isGrounded || currentNrOfJumps > 0)) 
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, Mathf.Lerp(jumpForce, 0, currentAirTime));
            jump = false;
            currentAirTime -= Time.fixedDeltaTime;
        }

        if (isGrounded)
        {
            currentNrOfJumps = nrOfJumps;
        }

        if(dashAvailable && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
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
        rbPlayer.AddForce(new Vector2(dashSpeed * horizontal, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        rbPlayer.gravityScale = gravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashAvailable = true;
    }
}
