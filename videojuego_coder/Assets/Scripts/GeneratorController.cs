using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public GameObject[] balasPrefab; //array con los prefab de las balas
    public float disparoSpeedInit = 1f; //velocidad inicial del disparador
    public float disparoSpeedRepetition = 0.2f; //tiempo que pasa entre una bala y la siguiente

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Disparo", disparoSpeedInit, disparoSpeedRepetition); //metodo invokerepeating, declaro el metodo como string, y luego su velocidad inicial y la velocidad de repeticion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo para disparar las balas
    void Disparo() 
    {
        int balasIndex = Random.Range(0, balasPrefab.Length); //declaro un rango entre 0 y el largo del array de balas para inicializarlas de manera aleatoria
        Instantiate(balasPrefab[balasIndex], transform); //metodo instantiate para inicializar las balas a partir del array y el transform del padre. 
    }
}
