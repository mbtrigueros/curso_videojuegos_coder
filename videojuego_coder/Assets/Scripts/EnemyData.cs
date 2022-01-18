using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data")]


public class EnemyData : ScriptableObject
{
    [SerializeField]
    protected int enemyLives = 2;
    
    [SerializeField]
    private float speedEnemy;

    [SerializeField]
    private float speedRunEnemy;

    public int EnemyLives
    {
        get { return enemyLives; }
    }

    public int EnemyLivesDown()
    {
        return enemyLives--; 
    }

    public void EnemyDead(GameObject enemy)
    {
        if (enemyLives <= 0)
        {
            enemy.SetActive(false);
        }
        
    }



    public float SpeedEnemy
    {
        get { return speedEnemy; }
    }

    public float SpeedRunEnemy
    {
        get { return speedRunEnemy; }
    }

}
