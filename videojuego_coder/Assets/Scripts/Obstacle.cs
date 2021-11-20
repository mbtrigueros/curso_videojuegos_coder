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
        timeObstacle -= Time.deltaTime; //al tiempo de la bala le voy restando el tiempo del juego, asi logro determinar si la bala sera destruida o no.
        if (timeObstacle > 0) //si el tiempo de la bala es mayor a 0, esta se movera
        {
            Movement();
        }
        else // si el tiempo llega a 0 sera destruida
        {
            Debug.Log("Destruyo el obstaculo");
            Destroy(gameObject);
        }
    }

    private Vector3 Movement()
    {
        return (transform.position += Vector3.up * speedObstacle) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Te moristeeeeeeeeeeeeee");        }
    }

}
