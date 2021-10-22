using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public GameObject balasPrefab; //array con los prefab de las balas
    public float disparoSpeedInit = 1f; //velocidad inicial del disparador
    public float disparoSpeedRepetition = 0.2f; //tiempo que pasa entre una bala y la siguiente
    public float cooldown = 0.5f; //tiempo de descanso entre cada disparo
    public bool beenShot = false; //variable booleana que establece si la bala ha sido disparada o no
    float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
       // InvokeRepeating("Disparo", disparoSpeedInit, disparoSpeedRepetition); //metodo invokerepeating, declaro el metodo como string, y luego su velocidad inicial y la velocidad de repeticion
    }

    // Update is called once per frame
    void Update()
    {
        DisparoControlado(); //llamo al metodo en el update
    }

    //metodo para disparar las balas de manera automatica
    //void Disparo() 
    //{
      //  int balasIndex = Random.Range(0, balasPrefab.Length); //declaro un rango entre 0 y el largo del array de balas para inicializarlas de manera aleatoria
        //Instantiate(balasPrefab[balasIndex], transform); //metodo instantiate para inicializar las balas a partir del array y el transform del padre. 
    //}

    //metodo para disparar las balas mediante un input
    public void DisparoControlado() 
    {
        if (Input.GetKeyDown(KeyCode.Z) && !beenShot) //si el usuario apreta la tecla Z y una bala NO ha sido disparada, se instanciara una bala
        {
            beenShot = true; //se establece que ha sido disparado la bala como true
            Instantiate(balasPrefab, transform);
        }
        if (beenShot) //si beenShot es true, se empieza a contar el tiempo pasado
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed > cooldown) // si el tiempo pasado es mayor al establecido en el cooldown, se reinicia el contador, y la variable beenShot vuelve a ser falsa. De esta manera, se puede volver a instanciar una nueva bala.
        {
            beenShot = false;
            timePassed = 0;
        }

    }
}


