using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Enemy
{

    private GameObject player; //llamo al player para poder usarlo en el 
    [SerializeField] private LayerMask playerMask; //llamo a la capa player


    private bool playerSeen = false; //booleana para detectar si el player ha sido visto o no

    [SerializeField] protected GameObject origen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // con el metodo find busco al jugador
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDies();
        DetectPlayer(origen.transform);
        if (playerSeen)
        {
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

        Vector3 dir = (player.transform.position - transform.position);  //obtengo el vector entre la posicion del jugador y la del enemigo

        transform.position += enemyData.SpeedEnemy * dir.normalized * Time.deltaTime;  //el metodo normalized es para que me devuelva el vector normalizado, es decir que su magnitud sea 1

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
