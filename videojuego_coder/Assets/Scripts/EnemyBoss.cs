using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForwards();
    }

    void MoveForwards()
    {
        animEnemy.SetBool("bossAttack", true); //determino la variable como cierta para activar la animacion de correr

        transform.forward = Vector3.right;
        transform.position += enemyData.SpeedRunEnemy * transform.forward * Time.deltaTime;
    }
}
