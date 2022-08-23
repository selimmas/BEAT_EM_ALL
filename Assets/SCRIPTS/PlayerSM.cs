using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [SerializeField] PlayerState currentState;
    [SerializeField] Animator animator;
    [SerializeField] Transform playerGraphics;
    [SerializeField] float speed;
    [SerializeField] float attackDuration = 1f;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float health = 10f;
    [SerializeField] GameObject attackPoint;

    Rigidbody2D rb2d;
    Vector2 moveDirection;

    float stopAttackTime;
    float jumpTimer;

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
                attackPoint.SetActive(true);
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
                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontalInput, verticalInput);

        bool jumpButton = Input.GetButtonDown("Jump");
        bool attack = Input.GetButtonDown("Fire1");

        if(moveDirection.magnitude != 0 && horizontalInput < 0)
        {
            playerGraphics.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        } 
        
        if(moveDirection.magnitude != 0 && horizontalInput > 0)
        {
            playerGraphics.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        switch (currentState)
        {
            case PlayerState.IDLE:
                if(moveDirection.magnitude != 0)
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
                if (moveDirection.magnitude == 0)
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
                    if(moveDirection.magnitude == 0)
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
                jumpTimer += Time.deltaTime;

                float y = jumpCurve.Evaluate(jumpTimer);

                playerGraphics.localPosition = new Vector3(playerGraphics.localPosition.x, y * jumpHeight, playerGraphics.localPosition.z);

                if(y == 0)
                {
                    if (moveDirection.magnitude == 0)
                    {
                        TransitionToState(PlayerState.IDLE);
                    }
                    else
                    {
                        TransitionToState(PlayerState.WALK);
                    }
                }

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
                rb2d.velocity = Vector2.zero;

                animator.SetBool("WALK", false);
                break;
            case PlayerState.ATTACK:
                attackPoint.SetActive(false);
                break;
            case PlayerState.RUN:
                break;
            case PlayerState.DEATH:
                break;
            case PlayerState.JUMP:
                jumpTimer = 0;
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
        switch (currentState)
        {
            case PlayerState.IDLE:;
                break;
            case PlayerState.WALK:
                rb2d.velocity = moveDirection.normalized * speed;
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
        } ;
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("HURT");

        if (health < 0)
        {
            TransitionToState(PlayerState.DEATH);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
