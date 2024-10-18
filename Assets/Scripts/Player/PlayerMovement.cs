using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpHeight;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float WallJumpCoolDown;
    private float HorizontalInput;

    [Header("Layer Parameters")]
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask WallLayer;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip jumpsound;


    private void Awake()//called when the script instance is being loaded
    {
        //get reference
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()//player movement
    {
        //move left&right
        HorizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(HorizontalInput * Speed, body.velocity.y);

        //character sprite turn left&right
        if (HorizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (HorizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        //set animation parameter
        anim.SetBool("Run?", HorizontalInput != 0);
        anim.SetBool("Grounded?", IsGrounded());

        //jump &walljump
        if (WallJumpCoolDown > 0.2f)
        {
            body.velocity = new Vector2(HorizontalInput * Speed, body.velocity.y);

            //jump on the wall and stay for 1 second and then fall
            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 3;

            //press space
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space)&& IsGrounded())
                {
                    SoundFXManager.instance.PlaySound(jumpsound);
                }
            }
                
        }
        else
            WallJumpCoolDown += Time.deltaTime;
    }

    private void Jump()
    {
       
        if (IsGrounded()) //normal jump
        {
            body.velocity = new Vector2(body.velocity.x, JumpHeight);
            anim.SetTrigger("Jump");
            
        }
        else if(OnWall() && !IsGrounded())//if character is on the wall already 
        {
            if (HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 4);

            WallJumpCoolDown = 0;
            

        }

    }

    private bool IsGrounded()//raycast vector.down
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,GroundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()//raycast depend on the character's localScale
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, WallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()//when player is on the ground, player can attack 
    {
        return IsGrounded() && !OnWall();
    }
}
