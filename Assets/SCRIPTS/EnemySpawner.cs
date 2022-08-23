using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount = 10;
    [SerializeField] float spawnDelay = 2;

    // Update is called once per frame
    void Start()
    {
        GameManager.instance.IncrementEnemeyCount(enemyCount);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(enemyCount > 0)
        {
            GameObject enemey = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemey.GetComponent<EnemySM>().targuet = player;

            enemyCount--;

            yield return new WaitForSeconds(spawnDelay);
        }
        
    }
}
