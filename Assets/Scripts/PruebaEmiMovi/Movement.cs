using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool betterJummp = false;
    public float runSpeed = 2;
    public float jumpSpeed = 2;

    public float fallMultiplier = 0.5f;

    public float lowJumpMultiplier = 1f;

    Rigidbody2D rb2D;

    SpriteRenderer sprite;

    Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();    
    }

   void FixedUpdate()
    {

        if (Input.GetKey("d") || Input.GetKey("right")){

            rb2D.linearVelocity = new Vector2(runSpeed, rb2D.linearVelocity.y);
            sprite.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey("a") || Input.GetKey("left")){

            rb2D.linearVelocity = new Vector2(-runSpeed, rb2D.linearVelocity.y);
            sprite.flipX = true;
            animator.SetBool("Run", true);
        }
        else{
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
            animator.SetBool("Run", false);
        }
        
        if (Input.GetKey("w") && CheckGround.OnGround){
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
        } 

        if(CheckGround.OnGround == false){
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        } else if (CheckGround.OnGround == true){
            animator.SetBool("Jump", false);
        }

        if(betterJummp){
            if(rb2D.linearVelocity.y<0 && !Input.GetKey("w")){
                rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }


            if(rb2D.linearVelocity.y>0 && !Input.GetKey("w")){
                rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }
}
