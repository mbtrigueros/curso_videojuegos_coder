using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{

    private GameObject player; //llamo al player para poder usarlo
    [SerializeField] private LayerMask playerMask; //llamo a la capa player


    private bool playerSeen = false; //booleana para detectar si el player ha sido visto o no

    [SerializeField] protected GameObject origen;
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
        EnemyDies();
        DetectPlayer(origen.transform);
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

        transform.position +=  enemyData.SpeedEnemy * transform.forward  * Time.deltaTime;  //el metodo normalized es para que me devuelva el vector normalizado, es decir que su magnitud sea 1
        if (dir.magnitude < 2f)
        {
            Walk();
            playerSeen = false;
        }

    }

    private void DetectPlayer(Transform origen) //metodo para detectar al player mediante raycast
    {
        Ray ray = new Ray(origen.position, origen.TransformDirection(Vector3.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10, playerMask))
        {
            playerSeen = true;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }

        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.blue);
        }
    }


}
