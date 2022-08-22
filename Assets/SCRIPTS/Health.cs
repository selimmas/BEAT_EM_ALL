using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("HURT");

        if (health < 0)
        {
            // Remplacer par transition to state death
            Destroy(gameObject);
        }
    }
}
