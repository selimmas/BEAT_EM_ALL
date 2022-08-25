using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject fleche;

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

        float xPosition = Mathf.Clamp(player.transform.position.x, 0, 106);

        transform.position = new Vector3(xPosition, 0, transform.position.z) ;
    }
}