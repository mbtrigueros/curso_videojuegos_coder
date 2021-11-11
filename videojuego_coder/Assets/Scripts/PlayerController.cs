using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables publicas
    [SerializeField] private int playerLives = 10; //cantidad de vidas del jugador
    [SerializeField] private float speedPlayer = 5f; //velocidad del jugador
    [SerializeField] private float rotationSpeed = 40f; // velocidad de rotacion 
    [SerializeField] private Vector3 initPosition = new Vector3(0, 0, 0); //posicion inicial del jugador
    [SerializeField] private Vector3 dir = new Vector3(-1, 0, 0); //direccion por default en la que va a moverse 
    private Material playerMaterial;
    private float a = 0.7f;
    private Color colorCollisionPlayer = Color.red;
    private Color colorPlayer = Color.yellow;

    // Start is called before the first frame update
    void Start() {
        //PlayerLivesDown(5); //verifico por metodo Start que mis metodos funcionen
        //PlayerLivesUp(2);
        Debug.Log("La cantidad de vidas es: " + playerLives); //muestro por consola la cantidad de vidas para ver que este todo ok

        playerMaterial = transform.GetComponent<MeshRenderer>().material;
        colorCollisionPlayer.a = 0.7f;
        colorPlayer.a = 1f;

    }

    // Update is called once per frame
    void Update() {

        PlayerMovementInput();
        PlayerRotation();

        if(playerLives == 0)
        {
            Destroy(gameObject);
        }

    }

    //Metodo para curar al jugador
    private int PlayerLivesUp(int lives) { //parametro que indica la cantidad de vidas que gana 
        return playerLives = playerLives + lives; //establezco la cantidad de vidas actuales
    }

    //Metodo para herir al jugador
    private int PlayerLivesDown(int lives) { //parametro que indica la cantidad de vidas que pierde
        return playerLives = playerLives - lives; //establezco la cantidad de vidas actuales
    }
    
    //Metodo para que el jugador se mueva
   /* private Vector3 PlayerMovement(Vector3 nuevaDir) { //paso como parametro la direccion que quiero que tome el jugador
        dir = nuevaDir; //reemplazo la dir inicial con el valor que pase como parametro
        return (transform.position +=  dir) * Time.deltaTime; //transforma la posicion del jugador en relacion a la direccion previamente designada. Agrego la funcion Time.deltaTime para que el movimiento sea constante
    } */

    //Metodo para que el jugador se mueva con el input del usuario. 
    private void PlayerMovementInput()
    {
        float ejeVertical = Input.GetAxis("Vertical"); //establecemos el eje vertical con getaxis
        transform.Translate(speedPlayer * Time.deltaTime * new Vector3(0, 0, ejeVertical)); //solo se pasa en el vector el eje vertical (teclas W y S), porque el eje horizontal sera utilizado para la rotacion
    }

    private void PlayerRotation()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal"); //establecemos el eje horizontal con getaxis
        transform.Rotate(Vector3.up, ejeHorizontal * rotationSpeed * Time.deltaTime); //con el metodo rotate, el se genera un quaternion que permite que el jugador rote. Esta rotacion sera sobre el eje y (vector3.up), asi logramos que el jugador rote como es conveniente. Se controla esta rotacion con el input del eje x, es decir con las teclas A y D. 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerLivesDown(2);
            Debug.Log("La cantidad de vidas es: " + playerLives);
            playerMaterial.SetColor("_Color", colorCollisionPlayer);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerMaterial.SetColor("_Color", colorPlayer);
        }
    }

}


