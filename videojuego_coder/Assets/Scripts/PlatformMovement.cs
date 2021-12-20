using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{


    [SerializeField] Transform[] waypoints; //waypoints hacia los que se movera la plataforma
    [SerializeField] float minDistance = 0.2f; //distancia minima
    [SerializeField] float speedPlatform = 2f; //velocidad de la plataforma

    private int currentIndex = 0; 
    private bool goBack = false; //variable booleana para establecer si vuelvo o no


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------

    void Move() //metodo para mover la plataforma a traves de waypoints
    {
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position; //vector entre la posicion de la plataforma y el waypoint
        Vector3 direction = deltaVector.normalized; //normalizacion del vector

        transform.position += direction * speedPlatform * Time.deltaTime; //muevo la plataforma en esa direccion

        float distance = deltaVector.magnitude; //magnitud del vector 

        if (distance < minDistance) //si la distancia es menor a la distancia minima establecida
        {
            if (!goBack) 
            {
                for (currentIndex = 0; currentIndex >= waypoints.Length - 1; currentIndex++) //recorro el array de waypoints y determino de acuerdo a si el indice es 0 o del tamaño del array, si tengo que sumar o restar al indice
                {
                    transform.position += direction * speedPlatform * Time.deltaTime;
                }
                goBack = true;
            }

            else 
            {
                for (currentIndex = waypoints.Length - 1; currentIndex <= 0; currentIndex--)
                {
                    transform.position += direction * speedPlatform * Time.deltaTime;
                }
                goBack = false;
            }
        }
    }

    //--------------------------------------------------------------------COLISIONES--------------------------------------------------------------------


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Estoy en la plataforma");

            float playerX = collision.transform.position.x;
            float platformX = transform.position.x;

            Move(); //solo va a moverse si el player esta en la plataforma
            collision.transform.position = new Vector3(transform.position.x, collision.transform.position.y, collision.transform.position.z); //para que el player se mueve junto con la plataforma
            
        }
    }
}
