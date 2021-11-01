using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum RotationTypes { follow, lookAt }; //enum para declarar los tipos de rotacion que hara el enemigo

    [SerializeField] private RotationTypes rotationtype; //variable del tipo de rotacion
    [SerializeField] private float speedEnemy = 4.0f; //velocidad a la que se mueve el enemigo

    private GameObject player; //llamo al player para poder usarlo en el script

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // con el metodo find busco al jugador
    }

    // Update is called once per frame
    void Update()
    {
        //switch para poder elegir desde el inspector el tipo de rotacion que quiero que tenga el enemigo
        switch (rotationtype)
        {
            case RotationTypes.follow:
                LookAt(player); //en el tipo de movimiento follow, paso ambos metodos; con lookAt logro que el enemigo rote en direccion al jugador
                Follow(2); // y en el metodo follow logro que se mueva en esa direccion
                break;
            case RotationTypes.lookAt:
                LookAt(player);
                break;
        }
    }

    private void LookAt(GameObject lookObject) //metodo para mirar al jugador. paso como parametro un gameObject para poder cambiar el target de ser necesario.
    {
        Quaternion newRotation = Quaternion.LookRotation(lookObject.transform.position - transform.position); //creo un quaternion con el metodo LookRotation y paso como parametro dos posiciones, la del enemigo y la del objeto. 
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1f * Time.deltaTime); //el metodo Lerp logra que la rotacion sea mas suave
    }

    private void Follow(int distancia) //metodo para seguir al jugador. paso como parametro la distancia a la que quiero que este del jugador. 
    {
        Vector3 dir = (player.transform.position - transform.position);  //obtengo el vector entre la posicion del jugador y la del enemigo

        if (dir.magnitude > distancia) //con el metodo magnitude obtengo el largo del vector, y calculo si es mayor que la distancia que quiero.
        {
            transform.position += speedEnemy * dir.normalized * Time.deltaTime;  //el metodo normalized es para que me devuelva el vector normalizado, es decir que su magnitud sea 1
        }
    }

}
