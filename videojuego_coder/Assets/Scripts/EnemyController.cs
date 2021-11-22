using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum BehaviourTypes { follow, walk }; //enum para declarar los tipos de comportamiento que hara el enemigo
    [SerializeField] private BehaviourTypes behaviourtype; //variable del tipo de comportamiento

    public int enemyLives = 50; //cantidad de vidas del enemigo. lo puse public para que pueda ser afectado por el script attack

    [SerializeField] private float speedEnemy = 4.0f; //velocidad a la que se mueve el enemigo

    private GameObject player; //llamo al player para poder usarlo en el script

    [SerializeField] private LayerMask playerMask; //llamo a la capa player

    [SerializeField] Transform[] waypoints; //waypoints hacia los que se movera la plataforma
    [SerializeField] float minDistance; //distancia minima
    private int currentIndex = 0;
    private bool goBack = false; //variable booleana para establecer si vuelvo o no

    [SerializeField] private Animator animEnemy; //animacion del enemigo 
    
    [SerializeField] private GameObject enemyCeiling; //variable para llamar al enemigo que esta en el techo

    private Rigidbody rbEnemy; //rigidbody del enemigo

    private bool playerSeen = false; //booleana para detectar si el player ha sido visto o no

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // con el metodo find busco al jugador


        animEnemy.SetBool("isWalking", true); //determino las variables de animacion de correr como falsa por default y la de caminar como verdadera
        animEnemy.SetBool("playerSeen", false);

        enemyCeilingRotation(); //giro al enemigo del techo 

        rbEnemy = GetComponent<Rigidbody>(); //rigidbody enemigo
    }

    // Update is called once per frame
    void Update()
    {

        
        DetectPlayer();
        EnemyDies();

        //switch para poder elegir desde el inspector el tipo de comportamiento que quiero que tenga el enemigo

         switch (behaviourtype) 
         {
             case BehaviourTypes.follow:
                if (playerSeen && rbEnemy.useGravity == true)
                {
                    Chase();
                }

                else
                {
                    Walk();
                }
                 break;
             case BehaviourTypes.walk:
                 Walk();
                 break;
         }
    }


    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------

    //--------------------------------------------------------------------ENEMY HEALTH

    private int EnemyLivesDown(int lives)
    { //parametro que indica la cantidad de vidas que pierde
        return enemyLives = enemyLives - lives; //establezco la cantidad de vidas actuales
    }

    //metodo para destruir al enemigo
    private void EnemyDies()
    {
        if (enemyLives <= 0)
        {
            Destroy(gameObject);
        }
    }

    //--------------------------------------------------------------------MOVIMIENTO

    //metodo para rotar al enemigo que esta en el techo
    private void enemyCeilingRotation()
    {
        enemyCeiling.transform.Rotate(new Vector3(0, 0, -180), Space.Self);
    }

    //metodo de caminata por waypoints 
    private void Walk()
    {
        animEnemy.SetBool("isWalking", true);
        animEnemy.SetBool("playerSeen", false);

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position; //vector entre la posicion del enemigo y el waypoint
        Vector3 direction = deltaVector.normalized; //normalizacion del vector

        transform.forward = direction * Time.deltaTime; //modifico el forward para que el frente del enemigo coincida con la direccion

        transform.position += transform.forward * speedEnemy * Time.deltaTime; //muevo el enemigo en esa direccion


        float distance = deltaVector.magnitude; //magnitud del vector 

        if (distance < minDistance) //si la distancia es menor a la distancia minima establecida
        {
            if (distance < minDistance) //si la distancia es menor a la distancia minima establecida
            {
                if (!goBack)
                {
                    for (currentIndex = 0; currentIndex >= waypoints.Length - 1; currentIndex++) //recorro el array de waypoints y determino de acuerdo a si el indice es 0 o del tamaño del array, si tengo que sumar o restar al indice
                    {
                        transform.position += direction * speedEnemy * Time.deltaTime;
                    }
                    goBack = true;
                }

                else
                {
                    for (currentIndex = waypoints.Length - 1; currentIndex <= 0; currentIndex--)
                    {
                        transform.position += direction * speedEnemy * Time.deltaTime;
                    }
                    goBack = false;
                }
            }
        }
    }

        //metodo para perseguir al enemigo
        private void Chase() 
    {
        animEnemy.SetBool("playerSeen", true); //determino la variable como cierta para activar la animacion de correr

        Vector3 dir = (player.transform.position - transform.position);  //obtengo el vector entre la posicion del jugador y la del enemigo
        
        transform.position += speedEnemy * dir.normalized * Time.deltaTime;  //el metodo normalized es para que me devuelva el vector normalizado, es decir que su magnitud sea 1
        
    }

    private void DetectPlayer() //metodo para detectar al player mediante raycast
    {
        Ray ray = new Ray(transform.position, transform.forward);
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
