using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data")]

public class EnemyData : ScriptableObject
{
    [SerializeField]
    protected int enemyLives;
    
    [SerializeField]
    private float speedEnemy;


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
            Destroy(enemy);
        }
        
    }


    public float SpeedEnemy
    {
        get
        {
            return speedEnemy;
        }
    }

}
