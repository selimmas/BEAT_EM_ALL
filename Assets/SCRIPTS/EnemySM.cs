using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : MonoBehaviour
{
    [SerializeField] EnemyState currentState;
    [SerializeField] Animator animator;
    [SerializeField] Transform enemyGraphics;
    [SerializeField] float speed;
    [SerializeField] float attackDuration = 3f;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float health = 20f;
    [SerializeField] Transform targuet;

    Rigidbody2D rb2d;
    Vector2 moveDirection;

    float stopAttackTime;
    bool attack;


    public enum EnemyState
    {
        IDLE,
        WALK,
        ATTACK,
        DEATH,

    }



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentState = EnemyState.IDLE;

        OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }


    void OnStateEnter()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.WALK:

                animator.SetBool("WALK", true);
                break;
            case EnemyState.ATTACK:
                stopAttackTime = Time.time + attackDuration;
                animator.SetTrigger("ATTACK");
                break;
            case EnemyState.DEATH:
                animator.SetTrigger("DEATH");
                break;
        }
    }
    void OnStateUpdate()
    {

        moveDirection = targuet.position - transform.position;
        float distance = Vector2.Distance(targuet.position, transform.position);


        if (moveDirection.x < 0)
        {
            enemyGraphics.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            enemyGraphics.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        switch (currentState)
        {
            case EnemyState.IDLE:
                if (moveDirection.magnitude != 0)
                {
                    TransitionToState(EnemyState.WALK);
                }
                if (attack == true)
                {
                    TransitionToState(EnemyState.ATTACK);

                }
                if (health <= 0)
                {
                    TransitionToState(EnemyState.DEATH);
                }
                break;
            case EnemyState.WALK:
                rb2d.velocity = moveDirection.normalized * speed;

                if (moveDirection.magnitude == 0)
                {
                    TransitionToState(EnemyState.IDLE);
                }
                if (distance < 1)
                {
                    TransitionToState(EnemyState.IDLE);

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
            case EnemyState.IDLE:
                break;


            case EnemyState.WALK:
                rb2d.velocity = Vector2.zero;

                animator.SetBool("WALK", false);
                break;
            case EnemyState.ATTACK:
                break;

            case EnemyState.DEATH:
                break;
            default:
                break;
        }
    }

    void TransitionToState(EnemyState nextState)
    {
        OnStateExit();

        currentState = nextState;

        OnStateEnter();
    }

    void OnStateFixedUpdate()
    {

        switch (currentState)
        {
            case EnemyState.IDLE:
                ;
                break;
            case EnemyState.WALK:
                break;
            case EnemyState.ATTACK:
                break;
            case EnemyState.DEATH:
                break;
            default:
                break;
        }
    }
}


