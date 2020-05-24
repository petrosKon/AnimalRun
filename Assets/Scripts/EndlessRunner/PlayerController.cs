using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public float jumpTime;
    public float speedIncreaseMultiplier;
    public float speedIncreaseMilestone;

    [Header("Ground")]
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    [Header("Audios")]
    public AudioSource jumpSound;
    public AudioSource deathSound;

    private Rigidbody2D myRigidbody;
    [HideInInspector]
    public Animator myAnimator;
    private float jumpTimeCount;
    private float speedMilestoneCount;
    private bool stoppedJumping;
    private bool canDoubleJump;
    private Vector2 playerPos;
    private float killCountdown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;

        myRigidbody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCount = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        stoppedJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);

        //this snippet kills player if he stucks in a wall for more than 3 seconds
        if ((Vector2) transform.position == playerPos)
        {
            killCountdown -= Time.deltaTime;
            if(killCountdown < 0)
            {
                Die();
            }
        }
        else
        {
            playerPos = transform.position;
            killCountdown = 3f;
        }

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone *= speedIncreaseMultiplier;

            moveSpeed *= speedIncreaseMultiplier;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
            }

            if(!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCount = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if (jumpTimeCount > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCount -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCount = 0;
            stoppedJumping = true;
            jumpSound.Play();
        }

        if (grounded)
        {
            jumpTimeCount = jumpTime;
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KillBox"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSound.Play();
        myAnimator.SetTrigger("Death");

        StartCoroutine(GameManager.instance.RestartGame());
    }
}
