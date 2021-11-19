using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum BehaviourTypes { follow, walk }; //enum para declarar los tipos de rotacion que hara el enemigo

    [SerializeField] private BehaviourTypes behaviourtype; //variable del tipo de comportamiento
    [SerializeField] private float speedEnemy = 4.0f; //velocidad a la que se mueve el enemigo
    [SerializeField] float rotationSpeed;

    private GameObject player; //llamo al player para poder usarlo en el script

    [SerializeField] Transform[] waypoints;
    [SerializeField] float minDistance;

    [SerializeField] private Animator animEnemy;

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private GameObject enemyCeiling;

    private bool playerSeen = false;

    private int currentIndex = 0;
    private bool goBack = false;


    private Rigidbody rbEnemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // con el metodo find busco al jugador


        animEnemy.SetBool("isWalking", true);
        animEnemy.SetBool("playerSeen", false);

        enemyCeilingRotation();

        rbEnemy = GetComponent<Rigidbody>();
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


    private int EnemyLivesDown(int lives)
    { //parametro que indica la cantidad de vidas que pierde
        return GameManager.enemyLives = GameManager.enemyLives - lives; //establezco la cantidad de vidas actuales
    }
    

    private void Walk()
    {
        animEnemy.SetBool("isWalking", true);
        animEnemy.SetBool("playerSeen", false);
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        transform.forward = direction * Time.deltaTime;

        transform.position += transform.forward * speedEnemy * Time.deltaTime;


        float distance = deltaVector.magnitude;

        if (distance < minDistance)
        {
            if (currentIndex >= waypoints.Length - 1)
            {
                goBack = true;
            }
            else if (currentIndex <= 0)
            {
                goBack = false;
            }

            if (!goBack)
            {
                currentIndex++;
            }
            else currentIndex--;
        }
    }

    private void Chase()
    {
        animEnemy.SetBool("playerSeen", true);
        Vector3 dir = (player.transform.position - transform.position);  //obtengo el vector entre la posicion del jugador y la del 
        
        transform.position += speedEnemy * dir.normalized * Time.deltaTime;  //el metodo normalized es para que me devuelva el vector normalizado, es decir que su magnitud sea 1
        
        
    }

    private void DetectPlayer()
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

    private void enemyCeilingRotation()
    {
        enemyCeiling.transform.Rotate(new Vector3(0, 0, -180), Space.Self);
    }

    private void EnemyDies()
    {
        if (GameManager.enemyLives <= 0)
        {
            Destroy(gameObject);
        }
        
    }


}
