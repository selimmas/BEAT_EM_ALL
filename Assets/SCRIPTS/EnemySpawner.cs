using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount = 10;

    // Update is called once per frame
    void Update()
    {
        if(enemyCount == 0)
        {
            return;
        }

        GameObject enemey = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemey.GetComponent<EnemySM>().targuet = player;

        enemyCount--;
    }
}
