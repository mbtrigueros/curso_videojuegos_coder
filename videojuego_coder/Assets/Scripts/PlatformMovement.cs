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
            if (currentIndex >= waypoints.Length - 1) //y el indice del array es mayor o igual al tamaño total del array -1
            {
                goBack = true; //vuelvo
            }

            else if (currentIndex <= 0) //si el indice actual es menor o igual a 0 
            {
                goBack = false; //no vuelvo
            }

            if (!goBack) //si no vuelvo, el indice aumenta
            {
                currentIndex++;
            }

            else if (goBack) //si vuelvo, el indice disminuy
            {
                currentIndex--;
            }       
        }
    }

    //--------------------------------------------------------------------COLISIONES--------------------------------------------------------------------


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Estoy en la plataforma");
            
            Move(); //solo va a moverse si el player esta en la plataforma
            
        }
    }
}
