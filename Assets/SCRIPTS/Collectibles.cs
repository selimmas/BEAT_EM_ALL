using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touch�");

       

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            GameManager.instance.AddScore(score);

            Debug.Log("Cindy");
        }
    }
}
