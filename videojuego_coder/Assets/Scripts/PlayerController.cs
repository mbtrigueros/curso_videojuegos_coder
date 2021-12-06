using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //variables publicas

    [SerializeField] private float speedPlayer = 5f; //velocidad del jugador
    [SerializeField] private float forceJump = 50f; //fuerza del salto del jugador

    [SerializeField] private GameObject[] cameras; //llamo a las camaras virtuales

    [SerializeField] private ParticleSystem attack; //llamo al sistema de particulas que hara de ataque
    [SerializeField] private float cooldown = 0.5f; //tiempo de descanso entre cada ataque
    [SerializeField] private bool beenShot = false; //variable booleana que establece si el ataque ha sido disparado o no
    [SerializeField] private float timePassed = 0f;


    [SerializeField] private GameObject[] enemiesFloor; //llamo a los enemigos que estaran en el suelo
    [SerializeField] private GameObject[] enemiesCeiling; //lamo a los enemigos que estaran en el techo

    private Rigidbody rbPlayer; //rogodbody del player
    
    private bool isGrounded = true; //booleana para identificar si el jugador esta en el piso

    private bool mirror = false; //booleana que establece si estas en el espejo o no
    private Vector3 gravedad = new Vector3(0, -9.8f, 0); // valor habitual de la gravedad en juego
    private Vector3 gravedadMirror = new Vector3(0, 9.8f, 0); //valor invertido cuando atravesas el espejo


    [SerializeField] private Animator animPlayer; //animacion del player
    [SerializeField]  private GameObject playerMesh; //mesh del player

    public static event Action onPlayerDeath;

    // Start is called before the first frame update
    void Start() {


        Debug.Log(Physics.gravity);

        Debug.Log("La cantidad de vidas es: " + GameManager.playerLives); //muestro por consola la cantidad de vidas para ver que este todo ok

        rbPlayer = GetComponent<Rigidbody>();

        animPlayer.SetBool("isRunning", false); //determino las variables de animacion de correr y saltar como falsas por default
        animPlayer.SetBool("isJumping", false);

        foreach (GameObject enemy in enemiesCeiling) //recorro el array de enemigos del techo, y con un foreach le asigno a cada componente del array lo que pongo dentro del for
        {
            enemy.GetComponent<Rigidbody>().useGravity = false; //convierto el uso de gravedad de los enemigos en el techo a falso, asi no se caen :) 
        }

    }

    // Update is called once per frame
    void Update() {
            
        if(GameManager.playerLives == 0) 
        {
            OnPlayerDeath();
            Debug.Log("Has sido derrotadx por tus demonios :(");
        }

    }

    void FixedUpdate() //como estoy moviendo al jugador con las fuerzas del rigidbody, llamo a los metodos en el fixedupdate
    {
        PlayerMove();
        PlayerJump();
        PlayerAttack();

    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------

    //--------------------------------------------------------------------EVENTS

    public static void OnPlayerDeath()
    {
        onPlayerDeath?.Invoke();
    }

    //--------------------------------------------------------------------PLAYER HEALTH Y SCORE

    //Metodo para curar al jugador
    private int PlayerLivesUp(int lives)
    { //parametro que indica la cantidad de vidas que gana 
        return GameManager.playerLives = GameManager.playerLives + lives; //establezco la cantidad de vidas actuales
    }

    //Metodo para herir al jugador
    private int PlayerLivesDown(int lives)
    { //parametro que indica la cantidad de vidas que pierde
        return GameManager.playerLives = GameManager.playerLives - lives; //establezco la cantidad de vidas actuales
    }

    public int GetPlayerLives()
    {
        return GameManager.playerLives;
    }

    private int PlayerStarsUp()
    { //parametro que indica la cantidad de vidas que gana 
        return GameManager.playerStars++;
    }

    //--------------------------------------------------------------------MOVIMIENTO Y ATAQUE

    //Metodo para que el jugador se mueva con el input del usuario. 
    private void PlayerMove()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal"); //establecemos el eje horizontal con getaxis
        rbPlayer.velocity = new Vector3(ejeHorizontal * speedPlayer, rbPlayer.velocity.y, 0); //modifico la velocidad del jugador para poder moverlo a traves del input. en el ejehorizontal estara el input, en el vertical queda igual(10 unidades por segundo) y en el z es 0. esto es para poder moverlo unicamente en los ejes horizontal y vertical, y no en el eje z.

        transform.forward = new Vector3(ejeHorizontal, 0, (Mathf.Abs(ejeHorizontal) - 1)); //con este metodo transformo la direccion en la que mira el jugador. paso el input como parametro horizontal, 0 al vertical, y en el eje z saco el absoluto del horizontal (es decir el modulo) y le resto 1. 

        //determino si el jugador se mueve, y le asigno true a la variable booleana para correr
        if (ejeHorizontal != 0)
        {
            animPlayer.SetBool("isRunning", true);
        }

        else
        {
            animPlayer.SetBool("isRunning", false);
        }

    }

    //metodo para que el jugador salte al apretar Z
    private void PlayerJump()
    {
        //declaro variables tanto para el salto en el contexto normal como en el espejo.
        Vector3 jump = Vector3.up; //vector up es 1 en el eje y
        Vector3 jumpMirror = Vector3.down; //vector down es -1 en el eje y

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded) //si apreto Z y estoy en el piso
        {
            animPlayer.SetBool("isJumping", true); //animacion para que el jugador salte
            if (mirror) //si esta en el espejo
            {
                Debug.Log("Salto en el mirror");
                rbPlayer.AddForce(jumpMirror * forceJump, ForceMode.Impulse); //aplico fuerza en el vector jumpMirror. el modo de la fuerza es de tipo impulso para generar mejor el moviemiento 
            }

            else //si no esta en el espejo
            {
                Debug.Log("Salto");
                rbPlayer.AddForce(jump * forceJump, ForceMode.Impulse); //aplico fuerza en el vector jump
            }
        }

        else
        {
            animPlayer.SetBool("isJumping", false); //si no apreto Z o si no estoy en el piso, la animacion es false
        }
    }


    //metodo para que el jugador ataque al apretar X
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && !beenShot) //si aprete X y beenShot es false
        {
            beenShot = true;
            attack.Play(); //disparo las particulas
        }

        if (beenShot) //si beenShot es true, se empieza a contar el tiempo pasado
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed > cooldown) // si el tiempo pasado es mayor al establecido en el cooldown, se reinicia el contador, y la variable beenShot vuelve a ser falsa
        {
            beenShot = false;
            timePassed = 0;
        }
    }

    //--------------------------------------------------------------------ESPEJO

    //metodo al atravesar el espejo
    private void enterUpsideDown()
    {

        Debug.Log("Cruce el espejo");

        playerMesh.transform.Rotate(new Vector3(0, 0, -180), Space.Self); //roto y bajo el personaje para que este alineado con el piso
        playerMesh.transform.position += new Vector3(0, 2.69f, 0);

        cameras[0].SetActive(false); //cambio las camaras
        cameras[1].SetActive(true);

        Physics.gravity = gravedadMirror; //invierto la gravedad
        mirror = true;


        foreach (GameObject enemy in enemiesFloor) //convierto el uso de gravedad de los enemigos en el piso a falso, asi no se caen :) 
        {

            if (enemy != null) //primero chequeo que el enemigo exista sino me pincha todo
            {
                enemy.GetComponent<Rigidbody>().useGravity = false;
            }

            else
            {
                Debug.Log("Ya mataste a este enemigo");
            }

        }

        foreach (GameObject enemy in enemiesCeiling) //convierto el uso de gravedad de los enemigos en el techo a verdadero
        {

            if (enemy != null)
            {
                enemy.GetComponent<Rigidbody>().useGravity = true;
            }

            else
            {
                Debug.Log("Ya mataste a este enemigo");
            }

        }

    }

    //basicamente lo mismo pero al reves
    private void outOfUpsideDown()
    {

        Debug.Log("Volvi a la normalidad");

        playerMesh.transform.Rotate(new Vector3(0, 180f, 180f), Space.World);
        playerMesh.transform.position += new Vector3(0, -2.69f, 0);

        cameras[1].SetActive(false);
        cameras[0].SetActive(true);

        Physics.gravity = gravedad;
        mirror = false;

        foreach (GameObject enemy in enemiesFloor)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                Debug.Log("Ya mataste a este enemigo");
            }
        }

        foreach (GameObject enemy in enemiesCeiling)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                Debug.Log("Ya mataste a este enemigo");
            }
        }
    }

    //--------------------------------------------------------------------COLISIONES--------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.CompareTag("Enemy")) //si colisiona con un enemigo, pierde 2 vidas
        {
            PlayerLivesDown(2);
            Debug.Log("La cantidad de vidas es: " + GameManager.playerLives);
        }

        if (collision.gameObject.CompareTag("Trap")) //si colisiona con una trampa, muere
        {
            PlayerLivesDown(GameManager.playerLives);
            Debug.Log("Has muerto no tan estupidamente pero igual si");
        }
        
        if (collision.gameObject.CompareTag("Obstacle")) //si colisiona con el obstaculo generado por la fuente, muere
        {
            PlayerLivesDown(GameManager.playerLives);
            Debug.Log("Has muerto estupidamente");
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor")) //detecto que estoy en el piso para asi poder saltar luego
        {
            Debug.Log("Estoy tocando el piso");
            isGrounded = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) //detecto si deje de estar en el piso para no poder saltar 
        {
            Debug.Log("NO estoy tocando el piso");
            isGrounded = false;
        }
    }

    //--------------------------------------------------------------------TRIGGERS--------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Star")) //si toco las estrellas las "agarro" 
        {
            PlayerStarsUp();
            Debug.Log("Tienes " +  GameManager.playerStars + " estrellas");
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Mirror") && !mirror) //atravieso el espejo y paso al techo
        {
            enterUpsideDown();
            
        }
        
        else if(other.gameObject.CompareTag("Mirror") && mirror) //vuelvo a atravesar el espejo y retorno a la normalidad
        {
            outOfUpsideDown();
        }
    }

    
}


