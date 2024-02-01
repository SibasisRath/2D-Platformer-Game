using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private Animator animator;
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] float speed;
    [SerializeField] int jumpForce;
    [SerializeField] PlayerScoreManager scoreManager;

    [SerializeField] private LayerMask m_WhatIsGround;       // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;        // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;		// A position marking where to check for ceilings

    private Transform lastCheckPoint;

    // [SerializeField] float crouchSpeed;
    //[SerializeField] int score = 0;
    //[SerializeField] TextMeshProUGUI scoreText;

    private Rigidbody2D rb;
    private BoxCollider2D playerBoxCollider;

    private Vector2 originalColliderCenter;
    private Vector2 originalColliderSize;

    bool flip;
    bool _isCrouching;

    [SerializeField] bool _canDoubleJump;

    [SerializeField] private bool _isOnGround;
    [SerializeField] private bool _isCelingPresent;

    

    public Transform LastCheckPoint { get => lastCheckPoint; set => lastCheckPoint = value; }


    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        animator = GetComponent<Animator>();
        playerBoxCollider = GetComponent<BoxCollider2D>();

        originalColliderCenter = playerBoxCollider.offset; //for 3d this property will be coliderObject.center
        originalColliderSize = playerBoxCollider.size;



        flip = false;
        _isCrouching = false;
        _isOnGround = false;
       
        _canDoubleJump = false;
    }

    void FixedUpdate()
    {
        GroundAndCelingCheck();
        
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        MovementFunc();

        Crouch();

        Jump();
    }

    #region Ground and Celing Check
    void GroundAndCelingCheck()
    {
        bool wasGrounded = _isOnGround;
        _isOnGround = false;

        float groundDistance = 0.2f;
        float ceilingDistance = 0.2f;
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, groundDistance, m_WhatIsGround);
        
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != gameObject)
            {
                _isOnGround = true;
                if (wasGrounded == false)
                {
                    SoundManager.Instance.Play(Sounds.PlayerLand);
                }
            }
        }

        if (Physics2D.OverlapCircle(m_CeilingCheck.position, ceilingDistance, m_WhatIsGround))
        {
            _isCelingPresent = true;

            //Debug.Log(Physics2D.OverlapCircle(m_CeilingCheck.position, ceilingDistance, m_WhatIsGround).gameObject.name);
        }
        else
        {
            _isCelingPresent = false;
        }

      
    }
    #endregion

    #region Jump function
    void Jump()
    {
        if (_isOnGround)
        {
            _canDoubleJump = true;
            if (verticalInput > 0)
            {
                animator.SetTrigger("Jump");
                SoundManager.Instance.Play(Sounds.PlayerJump);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            if (_canDoubleJump && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                animator.SetTrigger("Jump");
                SoundManager.Instance.Play(Sounds.PlayerJump);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                _canDoubleJump = false;
            }
        }
    }

    #endregion
   


    #region Crouch Function
    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (!_isCrouching)
            {
                _isCrouching = true;
                animator.SetBool("Crouch", true);

                //changing size ad position of player's colider
                playerBoxCollider.offset = new Vector2(playerBoxCollider.offset.x, playerBoxCollider.offset.y / 2f);
                playerBoxCollider.size = new Vector2(playerBoxCollider.size.x, playerBoxCollider.size.y / 2f);
            }
        }
        else if (_isCrouching && _isCelingPresent)
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            _isCrouching = false;
            animator.SetBool("Crouch", false);
            // Restore the original collider size and position when standing up
            playerBoxCollider.offset = originalColliderCenter;
            playerBoxCollider.size = originalColliderSize;
        }
    }

    #endregion

    #region Movement Fuction
    void MovementFunc()
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

            /* if (Mathf.Approximately(horizontalInput, 0f))
             {
                 flip = false;
             }*/

            GetComponent<SpriteRenderer>().flipX = flip;


            Vector3 movement = transform.position;
            movement.x += speed * horizontalInput * Time.deltaTime;
            transform.position = movement;
            SoundManager.Instance.Play(Sounds.PlayerMove);
        }
    }
    #endregion

    #region Collectables and Score
    public void PickUpCollectables(int points)
    {
        scoreManager.UpdateScore(points);
        //score += points;
        //scoreText.text = "Score: " + score;
    }

    #endregion
}
