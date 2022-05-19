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



    [Header("PositionCheck")]
    public float checkRadius;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public LayerMask whatIsCeiling;
    public Transform ceilingCheck;

    private bool headBumped = false;
    private bool isGrounded = false;



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
        //Debug.Log(isCrouching);
        Debug.Log(headBumped);
        
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
        headBumped = Physics2D.OverlapCircle(ceilingCheck.position, checkRadius, whatIsCeiling);
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




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(ceilingCheck.position, checkRadius);
    }









}
