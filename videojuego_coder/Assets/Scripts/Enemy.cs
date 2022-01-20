using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour

{

    
    [SerializeField] protected EnemyData enemyData;

    [SerializeField] Transform[] waypoints; //waypoints hacia los que se movera el enemigo
    [SerializeField] float minDistance; //distancia minima
    private int currentIndex = 0;
    private bool goBack = false; //variable booleana para establecer si vuelvo o no

    [SerializeField] protected Animator animEnemy; //animacion del enemigo 

    [SerializeField] public UnityEvent onEnemyDeath;


    // Start is called before the first frame update
    void Start()
    {

        animEnemy.SetBool("isWalking", true); //determino las variables de animacion de correr como falsa por default y la de caminar como verdadera
        animEnemy.SetBool("playerSeen", false);

    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        EnemyDies();

    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------

    //--------------------------------------------------------------------ENEMY HEALTH

    public int EnemyLivesDown()
    { 
        return enemyData.EnemyLivesDown(); 
    }

    public int GetEnemyLives()
    { 
        return enemyData.EnemyLives; //establezco la cantidad de vidas actuales
    }

    //metodo para destruir al enemigo
    public void EnemyDies()
    {
        if (GetEnemyLives() <= 0) onEnemyDeath?.Invoke();
    }

    //--------------------------------------------------------------------MOVIMIENTO

    //metodo de caminata por waypoints 
    public virtual void Walk()
    {
        animEnemy.SetBool("isWalking", true);
        animEnemy.SetBool("playerSeen", false);

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position; //vector entre la posicion del enemigo y el waypoint
        Vector3 direction = deltaVector.normalized; //normalizacion del vector

        transform.forward = direction * Time.deltaTime; //modifico el forward para que el frente del enemigo coincida con la direccion

        transform.Translate(Vector3.forward * enemyData.SpeedEnemy * Time.deltaTime); //muevo el enemigo en esa direccion


        float distance = deltaVector.magnitude; //magnitud del vector 

        if (distance < minDistance) //si la distancia es menor a la distancia minima establecida
        {
                if (!goBack)
                {
                    for (currentIndex = 0; currentIndex >= waypoints.Length - 1; currentIndex++) //recorro el array de waypoints y determino de acuerdo a si el indice es 0 o del tamaño del array, si tengo que sumar o restar al indice
                    {
                    
                        transform.position += direction * enemyData.SpeedEnemy * Time.deltaTime;
                    }
                    goBack = true;
                }

                else
                {
                    for (currentIndex = waypoints.Length - 1; currentIndex <= 0; currentIndex--)
                    {
                        transform.position += direction * enemyData.SpeedEnemy * Time.deltaTime;
                    }
                    goBack = false;

            }
        }
    }


    protected GameObject player; //llamo al player para poder usarlo
    [SerializeField] protected LayerMask playerMask; //llamo a la capa player


    protected bool playerSeen = false; //booleana para detectar si el player ha sido visto o no

    [SerializeField] protected GameObject origen;

    public virtual void DetectPlayer(Transform origen) //metodo para detectar al player mediante raycast
    {
        Ray ray = new Ray(origen.position, origen.TransformDirection(Vector3.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 7f, playerMask))
        {
            playerSeen = true;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }

        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 7f, Color.blue);
        }
    }

    //--------------------------------------------------------------------TRIGGERS--------------------------------------------------------------------

    public void onTriggerEnter (Collider other)
    {
        if ((other.CompareTag("Void") || other.CompareTag("Obstacle"))) onEnemyDeath?.Invoke();
    }

    //--------------------------------------------------------------------COLLISIONS

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap")) onEnemyDeath?.Invoke();
    }

}
