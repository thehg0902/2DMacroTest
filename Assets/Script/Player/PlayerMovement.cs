using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   





    [Header("MoveInputs")]
    public float speed;
    public int jumpForce;
    private float moveInput;
    private bool isCrouching;
    public float CrouchOffset;
    public float wallSlideSpeed;
    public float wallJumpForceX;
    public float wallJumpForceY;
    public float wallJumpTime;



    [Header("PositionCheck")]
    public float checkRadius;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform ceilingCheck;
    public Transform wallCheck;


    private bool headBumped = false;
    private bool isGrounded = false;
    private bool onWall = false;

    private bool wallSliding;
    private bool wallJumping;
    


    [Header("Sprite Values")]
    public Rigidbody2D rb2d;
    public GameObject sprite;
    private bool faceRight = true;

    [Header("PlayerInput")]
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Jump;
    public KeyCode Crowch;



    //sprite information
    private Vector2 colliderSize;
    private Vector3 spriteScale;

    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        colliderSize = sprite.GetComponent<BoxCollider2D>().size;
        spriteScale = sprite.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        p_Input();
        p_positionCheck();
    


        //Debug.Log(moveInput);
        //Debug.Log(faceRight);
    }
    void Update()
    {
        p_Jump();
        crowching();
        p_wallSlide();
        //Debug.Log(isCrouching);
        //Debug.Log(headBumped);
        //Debug.Log(wallSliding);
        //Debug.Log(wallJumping);
        //Debug.Log(wallJumpForceX);
        
    }

    void p_Input()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);

        if (faceRight == false && moveInput > 0)
        {
            flipCharacter();
        } else if (faceRight ==true && moveInput <0)
        {
            flipCharacter();
        }
    }

    void p_positionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        headBumped = Physics2D.OverlapCircle(ceilingCheck.position, checkRadius, whatIsGround);
        onWall     = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsGround);

    }




    void p_Jump()
    {
        
        if (Input.GetKeyDown(Jump) && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }
  
    void flipCharacter()
    {
        faceRight = !faceRight;
        Vector3 Scailer = transform.localScale;
        Scailer.x *= -1;
        transform.localScale = Scailer;
    }

    void crowching()
    {
        
        if (Input.GetKey(Crowch))
        {
            p_crowch(true);
        }
        else 
        {
            p_crowch(false);
        }
    }
    void p_crowch(bool crouch)
    {
        BoxCollider2D collider = sprite.GetComponent<BoxCollider2D>();
        
        if (crouch && !isCrouching)
        {
            isCrouching = true;
            //Debug.Log("CROWCH!!!");
            //make player shorter
            collider.size = new Vector2(colliderSize.x, colliderSize.y);
            sprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y * 0.6f, spriteScale.z);

            //sprite adjustment
            Vector3 position = sprite.transform.position;
            sprite.transform.position = new Vector3(position.x, position.y + 0.5f, position.z);

            position = transform.position;
            sprite.transform.position = new Vector3(position.x, position.y - CrouchOffset, position.z);



        }
        if (!crouch && isCrouching)
        {
            if (headBumped) { return; }
            //Debug.Log("CROWCH!!!");
            //make player taller
            isCrouching = false;
            collider.size = new Vector2(colliderSize.x, colliderSize.y );
            sprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, spriteScale.z);

            //sprite adjustment
            Vector3 position = sprite.transform.position;
            sprite.transform.position = new Vector3(position.x, position.y + 0.2f, position.z);
        }
    }

    void p_wallSlide()
    {
        if (onWall){ wallSliding = true;}else { wallSliding = false; }
        if (wallSliding)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        if(wallSliding && Input.GetKeyDown(Jump))
        {
            Debug.Log("JUMP!!!");
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping)
        {
            rb2d.velocity = new Vector2(wallJumpForceX * -moveInput, wallJumpForceY);
        }
  

    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }






    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(ceilingCheck.position, checkRadius);
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }









}
