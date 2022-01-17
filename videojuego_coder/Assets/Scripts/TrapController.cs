using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{


    [SerializeField] private LayerMask playerMask; //llamo a la capa del player
    [SerializeField] private Transform origen;

    private bool playerDetected = false; //booleana para la deteccion juegador

    private Rigidbody rbTrap; //traigo el rigidbody de la trampa


    // Start is called before the first frame update
    void Start()
    
    {
        playerDetected = false; 
        rbTrap = GetComponent<Rigidbody>();
        rbTrap.useGravity = false; //establezco que la gravedad es falsa para que no se caiga 
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer(origen);
    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------
    private void DetectPlayer(Transform origen) //metodo para detectar al jugador con raycast
    {
        Ray ray = new Ray(origen.position, origen.up); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, playerMask))
        {
            playerDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            rbTrap.isKinematic = false;
            rbTrap.useGravity = true;
        }

        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.blue);

        }

    }

}
