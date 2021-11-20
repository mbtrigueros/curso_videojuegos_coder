using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] private float speedObstacle = 2f; //velocidad del obstaculo
    [SerializeField] private float timeObstacle = 1f; //tiempo en que el obstaculo estara en la escena antes de ser eliminado

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeObstacle -= Time.deltaTime; //le voy tomando el tiempo al obstaculo para luego destruirlo

        if (timeObstacle > 0) //si el tiempo es mayor a cero, el obstaculo va a moverse
        {
            Movement();
        }

        else // si el tiempo llega a 0 sera destruido
        {
            Debug.Log("Destruyo el obstaculo");
            Destroy(gameObject);
        }
    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------
    private Vector3 Movement() //metodo para mover el obstaculo en el eje vertical
    {
        return (transform.position += Vector3.up * speedObstacle) * Time.deltaTime; 
    }


}
