using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Testmode toggle
    public bool TestMode = true;

    //Component
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Inspector balance variables
    [SerializeField] float speed = 7.0f;
    [SerializeField] int jumpForce = 10;

    //groundcheck stuff
    [SerializeField] bool isGrounded;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask isGroundLayer;
    [SerializeField] float groundCheckRadius = 0.02f;




    //lives and score
    [SerializeField] int maxLives = 5;
    
    private int _lives;
    public int lives
    {
        get => _lives;
        set 
        {
            _lives = value;

            if (TestMode) Debug.Log("Lives has been set to: " + lives.ToString());
        }
    }

    private int _score = 0;
    public int score
    {
        get => score;
        set
        {
            _score = value;
            if (TestMode) Debug.Log("Score has been set to: " + score.ToString());
        }


    }


    //Coroutine
    Coroutine jumpForceChange = null;


    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {

            jumpForceChange = StartCoroutine(JumpForceChange());
            return;
        }
        StopCoroutine(jumpForceChange);
        jumpForceChange = null;
        jumpForce /= 2;
        StartJumpForceChange();
    }


    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
        jumpForceChange = null;
    }





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 7.0f;
            if (TestMode) Debug.Log("Default Value of Speed has changed on " + gameObject.name);
        }

        if (jumpForce <= 0)
        {
            jumpForce = 7;
            if (TestMode) Debug.Log("Default Value of JumpForce has changed on " + gameObject.name);
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            if (TestMode) Debug.Log("Groundcheck radius value has been defaulted on " + gameObject.name);
        }


        if (GroundCheck == null)
        {
            //GroundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            GroundCheck = obj.transform;
            if (TestMode) Debug.Log("Groundcheck object was created on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);
        if (isGrounded)
        {
            rb.gravityScale = 1;
            anim.ResetTrigger("JumpAtk");
        }

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);

        //This is stopping you from moving when you fire
        if (clipInfo[0].clip.name == "Fire")
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("Fire");
            }
        }

        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Jump") && !isGrounded)
        {
            anim.SetTrigger("JumpAtk");
        }



        anim.SetFloat("Input", Mathf.Abs(xInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("Crouch", yInput); 

        //Sprite Flipping
        if (xInput != 0) sr.flipX = (xInput < 0);

        

    }



    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }




    //Trigger functions are called most times - but still generally require one physics body
    private void OnTriggerEnter2D(Collider2D other)
    {
       
      if (other.tag == "Collectible")
       {
          Destroy(other.gameObject);

       }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {    

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    //Collisions functions are only called when one of two objects that collide is a dynamic rigidbody
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }



}