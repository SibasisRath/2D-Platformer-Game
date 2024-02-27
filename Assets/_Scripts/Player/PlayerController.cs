using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private PlayerScoreManager scoreManager;

    [SerializeField] private LayerMask whatIsGround;       // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;        // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingCheck;		// A position marking where to check for ceilings

    private Transform lastCheckPoint;

    [SerializeField] private float crouchSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 originalColliderCenter;
    private Vector2 originalColliderSize;

    private bool flip;
    private bool isCrouching;

    [SerializeField] private bool canDoubleJump;

    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isCelingPresent;

    private const float groundDistance = 0.2f;
    private const float ceilingDistance = 0.2f;

    public Transform LastCheckPoint { get => lastCheckPoint; set => lastCheckPoint = value; }

    #endregion
    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        originalColliderCenter = playerBoxCollider.offset; 
        originalColliderSize = playerBoxCollider.size;

        flip = false;
        isCrouching = false;
        isOnGround = false;
       
        canDoubleJump = false;
    }

    private void FixedUpdate()
    {
        GroundAndCelingCheck();
        Jump();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        MovementFunc();

        Crouch();       
    }

    #region Ground and Celing Check
    private void GroundAndCelingCheck()
    {
        bool wasGrounded = isOnGround;
        isOnGround = false;
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundDistance, whatIsGround);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            isOnGround = true;
            if (wasGrounded == false)
            {
                animator.SetBool("Jump", false);
                SoundManager.Instance.Play(Sounds.PlayerLand);
            }
        }

        if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingDistance, whatIsGround))
        {
            isCelingPresent = true;
        }
        else
        {
            isCelingPresent = false;
        }      
    }
    #endregion

    #region Jump function
    private void Jump()
    {
        if (isOnGround)
        {
            canDoubleJump = true;
            if (verticalInput > 0)
            {
                animator.SetBool("Jump", true);
                SoundManager.Instance.Play(Sounds.PlayerJump);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            if (canDoubleJump && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                animator.SetBool("Jump",true);
                SoundManager.Instance.Play(Sounds.PlayerJump);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }

    #endregion
   
    #region Crouch Function
    private void Crouch()
    {
        float gettingHalf = 2f;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                animator.SetBool("Crouch", true);

                //changing size ad position of player's colider
                playerBoxCollider.offset = new Vector2(playerBoxCollider.offset.x, playerBoxCollider.offset.y / gettingHalf);
                playerBoxCollider.size = new Vector2(playerBoxCollider.size.x, playerBoxCollider.size.y / gettingHalf);
            }
        }
        else if (isCrouching && isCelingPresent)
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            isCrouching = false;
            animator.SetBool("Crouch", false);
            // Restore the original collider size and position when standing up
            playerBoxCollider.offset = originalColliderCenter;
            playerBoxCollider.size = originalColliderSize;
        }
    }

    #endregion

    #region Movement Fuction
    private void MovementFunc()
    {
        animator.SetFloat("Run", Mathf.Abs(horizontalInput));

        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            if (horizontalInput < 0)
            {
                flip = true;
            }
            else if (horizontalInput > 0)
            {
                flip = false;
            }

            spriteRenderer.flipX = flip;


            Vector3 movement = transform.position;
            if (isCrouching)
            {
                movement.x += crouchSpeed * horizontalInput * Time.deltaTime;
            }
            else
            {
                movement.x += speed * horizontalInput * Time.deltaTime;
                SoundManager.Instance.Play(Sounds.PlayerMove);
            }
            transform.position = movement;
            
        }
    }
    #endregion

    #region Collectables and Score
    public void PickUpCollectables(int points)
    {
        scoreManager.UpdateScore(points);
    }

    #endregion
}
