using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [SerializeField] PlayerState currentState;
    [SerializeField] Animator animator;
    [SerializeField] float speed;
    [SerializeField] float attackDuration = 1f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float health = 10f;

    Rigidbody2D rb2d;
    Vector2 moveDirection;
    Vector2 velocity;
    bool isGrounded;
    bool isAttacking;

    float stopAttackTime;

    public enum PlayerState
    {
        IDLE,
        WALK,
        ATTACK,
        RUN,
        DEATH,
        JUMP
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        isGrounded = true;

        currentState = PlayerState.IDLE;

        OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }

    private void FixedUpdate()
    {
        OnStateFixedUpdate();
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
               
                animator.SetBool("WALK", true);
                break;
            case PlayerState.ATTACK:
                stopAttackTime = Time.time + attackDuration;
                animator.SetTrigger("ATTACK");
                break;
            case PlayerState.RUN:
                break;
            case PlayerState.DEATH:
                animator.SetTrigger("DEATH");
                break;
            case PlayerState.JUMP:
                animator.SetTrigger("JUMP");
                // rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontalInput, verticalInput);

        bool jumpButton = Input.GetButtonDown("Jump");
        bool attack = Input.GetButtonDown("Fire1");

        switch (currentState)
        {
            case PlayerState.IDLE:
                if(verticalInput != 0 || horizontalInput != 0)
                {
                    TransitionToState(PlayerState.WALK);
                }

                if (jumpButton == true)
                {
                    TransitionToState(PlayerState.JUMP);
                }

                if(attack == true)
                {
                    TransitionToState(PlayerState.ATTACK);
                }

                if(health <= 0)
                {
                    TransitionToState(PlayerState.DEATH);
                }

                break;

            case PlayerState.WALK:
                if (horizontalInput == 0 && verticalInput == 0)
                {
                    TransitionToState(PlayerState.IDLE);
                }


                if (jumpButton == true)
                {
                    TransitionToState(PlayerState.JUMP);
                }

                if (attack == true)
                {
                    TransitionToState(PlayerState.ATTACK);
                }

                if (health <= 0)
                {
                    TransitionToState(PlayerState.DEATH);
                }

                break;
            case PlayerState.ATTACK:
                if(Time.time > stopAttackTime)
                {
                    if(horizontalInput == 0 && verticalInput == 0)
                    {
                        TransitionToState(PlayerState.IDLE);
                    } else
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                }

                if (health <= 0)
                {
                    TransitionToState(PlayerState.DEATH);
                }
                break;
            case PlayerState.RUN:
            case PlayerState.JUMP:
                if (health <= 0)
                {
                    TransitionToState(PlayerState.DEATH);
                }
                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
                
                animator.SetBool("WALK", false);
                break;
            case PlayerState.ATTACK:
                break;
            case PlayerState.RUN:
                break;
            case PlayerState.DEATH:
                break;
            case PlayerState.JUMP:
                break;
            default:
                break;
        }
    }

    void TransitionToState(PlayerState nextState)
    {
        OnStateExit();

        currentState = nextState;

        OnStateEnter();
    }

    private void OnStateFixedUpdate()
    {
        velocity = moveDirection.normalized * speed;

        rb2d.velocity = velocity;
    }
}
