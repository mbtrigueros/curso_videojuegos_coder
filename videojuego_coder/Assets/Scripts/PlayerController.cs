using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables publicas
    public int playerLives = 10; //cantidad de vidas del jugador
    public float speedPlayer = 0.5f; //velocidad del jugador
    public Vector3 initPosition = new Vector3(0, 0, 0); //posicion inicial del jugador
    public Vector3 dir = new Vector3(-1, 0, 0); //direccion por default en la que va a moverse 

    // Start is called before the first frame update
    void Start() {
        //PlayerLivesDown(5); //verifico por metodo Start que mis metodos funcionen
        //PlayerLivesUp(2);
        Debug.Log("La cantidad de vidas es: " + playerLives); //muestro por consola la cantidad de vidas para ver que este todo ok
    }

    // Update is called once per frame
    void Update() {
        //PlayerMovement(new Vector3(0, 0, speedPlayer)); //establezco por parametro la direccion en la que quiero que se mueva el jugador y verifco su funcionalidad
    }

    //Metodo para curar al jugador
    public int PlayerLivesUp(int lives) { //parametro que indica la cantidad de vidas que gana 
        return playerLives = playerLives + lives; //establezco la cantidad de vidas actuales
    }

    //Metodo para herir al jugador
    public int PlayerLivesDown(int lives) { //parametro que indica la cantidad de vidas que pierde
        return playerLives = playerLives - lives; //establezco la cantidad de vidas actuales
    }
    
    //Metodo para que el jugador se mueva
    public Vector3 PlayerMovement(Vector3 nuevaDir) { //paso como parametro la direccion que quiero que tome el jugador
        dir = nuevaDir; //reemplazo la dir inicial con el valor que pase como parametro
        return (transform.position +=  dir) * Time.deltaTime; //transforma la posicion del jugador en relacion a la direccion previamente designada. Agrego la funcion Time.deltaTime para que el movimiento sea constante
    }

}

