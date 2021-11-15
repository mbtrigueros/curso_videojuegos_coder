using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables publicas
    [SerializeField] private int playerLives = 10; //cantidad de vidas del jugador
    [SerializeField] private float speedPlayer = 5f; //velocidad del jugador
    [SerializeField] private float forceJump = 50f; //fuerza del salto del jugador

    [SerializeField] private float rotationSpeed = 40f; // velocidad de rotacion 
    [SerializeField] private Vector3 initPosition = new Vector3(0, 0, 0); //posicion inicial del jugador
    [SerializeField] private Vector3 dir = new Vector3(-1, 0, 0); //direccion por default en la que va a moverse 

    private Material playerMaterial;
    private float a = 0.7f;
    private Color colorCollisionPlayer = Color.red;
    private Color colorPlayer = Color.yellow;


    [SerializeField] private GameObject[] cameras;

    private GameObject[] enemies;
    private Rigidbody rbPlayer;
    
    private bool isGrounded = true;

    private bool mirror = false;
    private bool enteredMirror = false;
    private Vector3 gravedadMirror = new Vector3(0, 9.8f, 0);
    private Vector3 gravedad = new Vector3(0, -9.8f, 0);


    // Start is called before the first frame update
    void Start() {

        Debug.Log(Physics.gravity);

        Debug.Log("La cantidad de vidas es: " + playerLives); //muestro por consola la cantidad de vidas para ver que este todo ok

        playerMaterial = transform.GetComponent<MeshRenderer>().material;
        colorCollisionPlayer.a = 0.7f;
        colorPlayer.a = 1f;

        rbPlayer = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update() {

        PlayerMovementInput();
        // PlayerRotation();
            
        if(playerLives == 0)
        {
            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {

        PlayerJump();

    }

    //Metodo para curar al jugador
    private int PlayerLivesUp(int lives) { //parametro que indica la cantidad de vidas que gana 
        return playerLives = playerLives + lives; //establezco la cantidad de vidas actuales
    }

    //Metodo para herir al jugador
    private int PlayerLivesDown(int lives) { //parametro que indica la cantidad de vidas que pierde
        return playerLives = playerLives - lives; //establezco la cantidad de vidas actuales
    }
    

    //Metodo para que el jugador se mueva con el input del usuario. 
    private void PlayerMovementInput()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal"); //establecemos el eje vertical con getaxis
        transform.Translate(speedPlayer * Time.deltaTime * new Vector3(ejeHorizontal, 0, 0)); //solo se pasa en el vector el eje vertical (teclas W y S), porque el eje horizontal sera utilizado para la rotacion
    }

    private void PlayerJump()
    {
        Vector3 jump = Vector3.up;
        Vector3 jumpMirror = Vector3.down;
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            if (mirror)
            {
                Debug.Log("Aprete z en mirror");
                rbPlayer.AddForce(jumpMirror * forceJump, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Aprete z");
                rbPlayer.AddForce(jump * forceJump, ForceMode.Impulse);
            }
            
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerLivesDown(2);
            Debug.Log("La cantidad de vidas es: " + playerLives);
            playerMaterial.SetColor("_Color", colorCollisionPlayer);

        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerMaterial.SetColor("_Color", colorPlayer);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mirror") && !enteredMirror)
        {
            enterUpsideDown();
            enteredMirror = true;
        }
        else if(other.gameObject.CompareTag("Mirror") && enteredMirror)
        {
            outOfUpsideDown();
            enteredMirror = false;
        }
    }


    private void enterUpsideDown()
    {

            Debug.Log("Cruce el espejo");
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            Physics.gravity = gravedadMirror;
            mirror = true;

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Rigidbody>().useGravity = false;
            }

    }

    private void outOfUpsideDown()
    {

        Debug.Log("Volvi a la normalidad");
        cameras[1].SetActive(false);
        cameras[0].SetActive(true);
        Physics.gravity = gravedad;
        mirror = false;

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    //                                                                  COSAS QUE POR AHORA NO ESTOY USANDO


    /*private void PlayerRotation()
     {
         float ejeVertical = Input.GetAxis("Vertical"); //establecemos el eje horizontal con getaxis
         transform.Rotate(Vector3.up, ejeVertical * rotationSpeed * Time.deltaTime); //con el metodo rotate, el se genera un quaternion que permite que el jugador rote. Esta rotacion sera sobre el eje y (vector3.up), asi logramos que el jugador rote como es conveniente. Se controla esta rotacion con el input del eje x, es decir con las teclas A y D. 
     }*/

    //Metodo para que el jugador se mueva
    /* private Vector3 PlayerMovement(Vector3 nuevaDir) { //paso como parametro la direccion que quiero que tome el jugador
         dir = nuevaDir; //reemplazo la dir inicial con el valor que pase como parametro
         return (transform.position +=  dir) * Time.deltaTime; //transforma la posicion del jugador en relacion a la direccion previamente designada. Agrego la funcion Time.deltaTime para que el movimiento sea constante
     } */
}


