using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float speedBala = 0.5f; //velocidad de la bala
    public Vector3 dir = new Vector3(1, 0, 0); //direccion de la bala
    public int balaDamage = 1; //daño que provoca la bala
    public float timeBala = 3f; //tiempo en que la bala estara en la escena antes de ser eliminada

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        BalaNewScale(2); //llamo al metodo para multiplicar la escala de la bala

        timeBala -= Time.deltaTime; //al tiempo de la bala le voy restando el tiempo del juego, asi logro determinar si la bala sera destruida o no.
        if (timeBala > 0) //si el tiempo de la bala es mayor a 0, esta se movera
        {
            BalaMovement(new Vector3(speedBala, 0, 0));
        }
        else // si el tiempo llega a 0 sera destruida
        {
            Destroy(gameObject);
        }
    }

    //metodo para el movimiento de la bala
    public Vector3 BalaMovement(Vector3 nuevaDir) 
    {
        dir = nuevaDir;
        return (transform.position += dir) * Time.deltaTime;
    }

    //metodo para cambiar la escala de la bala
    public void BalaNewScale(int multiplo) //paso por parametro el nro por el cual quiero multiplicar la escala
    {

        if (Input.GetKeyDown(KeyCode.Space)) //si el usuario presiona la barra espaciadora, la escala de la bala se multiplica por el numero establecido en el parametro
        {
            transform.localScale += transform.localScale * multiplo;
        }
    }
    
}
