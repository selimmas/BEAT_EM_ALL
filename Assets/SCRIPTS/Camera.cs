using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

       

        transform.position = new Vector3(player.transform.position.x, 0, transform.position.z) ;



    }
}