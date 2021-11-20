using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteGenerator : MonoBehaviour
{

    [SerializeField] private GameObject obstacle; //traigo al prefab del obstaculo

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generator", 1f, 1f); //metodo invoke repeating para repetir la instanciacion del obstaculo
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------
    private void Generator()
    {
        Instantiate(obstacle, transform); //metodo para instanciar el obstaculo

    }
}
