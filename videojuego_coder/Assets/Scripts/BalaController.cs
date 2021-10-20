using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public float speedBala = 0.5f; //velocidad de la bala
    public Vector3 dir = new Vector3(1, 0, 0); //direccion de la bala
    public int balaDamage = 1; //daño que provoca la bala
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BalaMovement(new Vector3(speedBala, 0, 0)); //inicio el metodo de movimiento en el update
    }

    //metodo para el movimiento de la bala
    public Vector3 BalaMovement(Vector3 nuevaDir) 
    {
        dir = nuevaDir;
        return (transform.position += dir) * Time.deltaTime;
    }
}
