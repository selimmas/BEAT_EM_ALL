using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    int enemyCount;
    public int globalScore;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    
    public void IncrementEnemeyCount(int value)
    {
        enemyCount += value;
    }

    public void DecrementEnemeyCount()
    {
        enemyCount--;
    }

    public void AddScore(int value)
    {
        globalScore = globalScore + value;
    }

    public void Die()
    {
        Time.timeScale = 0;
    }

    IEnumerator Pause(float delay)
    {
        yield return new WaitForSeconds(delay);


    }
}
