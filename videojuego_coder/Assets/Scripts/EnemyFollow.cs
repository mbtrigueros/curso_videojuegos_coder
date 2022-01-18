using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{

    protected Rigidbody rbEnemy; //rigidbody del enemigo

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // con el metodo find busco al jugador
        rbEnemy = GetComponent<Rigidbody>(); //rigidbody enemigo
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer(origen.transform);
        EnemyDies();
        if (playerSeen && rbEnemy.useGravity == true) { 

            Chase();
        }

        else
        {
            Walk();
        }
        
    }

    //metodo para perseguir al enemigo
    private void Chase()
    {
        animEnemy.SetBool("playerSeen", true); //determino la variable como cierta para activar la animacion de correr

        float enemyX = transform.position.x; //posicions de x tanto del player como el enemigo, asi a la hora de modificar la direccion no da una vuelta en el aire :) 

        float playerX = player.transform.position.x;

        Vector3 dir = new Vector3(playerX, 0f, 0f) - new Vector3(enemyX, 0f, 0f);  //obtengo el vector entre la posicion del jugador y la del enemigo

        transform.forward = dir.normalized; //modifico el forward para que el frente del enemigo coincida con la direccion

        transform.position +=  enemyData.SpeedRunEnemy * transform.forward  * Time.deltaTime;  //el metodo normalized es para que me devuelva el vector normalizado, es decir que su magnitud sea 1
        if (dir.magnitude < 2f)
        {
            Walk();
            playerSeen = false;
        }

    }




}
