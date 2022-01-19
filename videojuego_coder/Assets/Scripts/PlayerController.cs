using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{
    //variables publicas

    [SerializeField] private float speedPlayer = 5f; //velocidad del jugador
    [SerializeField] private float forceJump = 50f; //fuerza del salto del jugador
    private bool pressedJump;

    [SerializeField] private GameObject[] cameras; //llamo a las camaras virtuales

    [SerializeField] private ParticleSystem attackEffect; //llamo al sistema de particulas que hara el efecto del dash
    [SerializeField] private ParticleSystem dashEffect; //llamo al sistema de particulas que hara el efecto del dash
    [SerializeField] private ParticleSystem impact; //llamo al sistema de particulas que hara de impacto al caer
    [SerializeField] private ParticleSystem footsteps; //llamo al sistema de particulas que hara de "polvo" al caminar


    [SerializeField] private GameObject[] enemiesFloor; //llamo a los enemigos que estaran en el suelo
    [SerializeField] private GameObject[] enemiesCeiling; //lamo a los enemigos que estaran en el techo
    private GameObject[] enemies;

    private Rigidbody rbPlayer; //rogodbody del player

    private bool isGrounded = true; //booleana para identificar si el jugador esta en el piso
    private bool wasGrounded = false; //booleana para identificar si el jugador estaba en el piso en el frame anterior

    private bool mirror = false; //booleana que establece si estas en el espejo o no
    private Vector3 gravedad = new Vector3(0, -9.8f, 0); // valor habitual de la gravedad en juego
    private Vector3 gravedadMirror = new Vector3(0, 9.8f, 0); //valor invertido cuando atravesas el espejo


    [SerializeField] private Animator animPlayer; //animacion del player
    [SerializeField] private GameObject playerMesh; //mesh del player

    GameObject globalPostProcessing;

    private float lastPostion;
    private Vector3 lastGrounded;
    private Vector3 groundedOffset = new Vector3(1.5f, 0f, 0f);


    //variables de eventos
    public static event Action onPlayerDeath;
    [SerializeField] private UnityEvent onPlayerFallen;
    [SerializeField] private UnityEvent onLevelChange;
    public static event Action<int> onPlayerLivesChange;
    public static event Action<int> onPlayerStarsChange;
    //public static event Action<int> onLevelCompleted;

    // Start is called before the first frame update
    void Start() {

        globalPostProcessing = GameObject.Find("GlobalPostProcessing");

        Debug.Log("Tenes estas vidas " + GetPlayerLives());
        Debug.Log("Tenes estas stars " + GetPlayerStars());

        Debug.Log(Physics.gravity);

        rbPlayer = GetComponent<Rigidbody>();

        animPlayer.SetBool("isRunning", false); //determino las variables de animacion de correr y saltar como falsas por default

        foreach (GameObject enemy in enemiesCeiling) //recorro el array de enemigos del techo, y con un foreach le asigno a cada componente del array lo que pongo dentro del for
        {
            enemy.GetComponent<Rigidbody>().useGravity = false; //convierto el uso de gravedad de los enemigos en el techo a falso, asi no se caen :) 
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();

        //OnAllEnemiesDeath();

        if (GameManager.playerLives == 0) {
            AudioManager.instance.PlaySound("Dead");
            OnPlayerDeath();
            Debug.Log("Has sido derrotadx por tus demonios :(");
        }


        var coyote = isGrounded ? coyoteTimeCounter = coyoteTime : coyoteTimeCounter -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Z) && coyoteTimeCounter > 0f)  //si apreto Z y estoy en el piso
        {
            AudioManager.instance.PlaySound("Jump");
            pressedJump = true; //esta variable es para que el input del jugador se maneje en el update y no el fixed update porque puede traer problemas de
            hasDoubleJumped = false;
            coyoteTimeCounter = 0f;

        }

        else if (Input.GetKeyDown(KeyCode.Z) && !isGrounded && !hasDoubleJumped) {
            AudioManager.instance.PlaySound("DoubleJump");
            doubleJump = true;
            hasDoubleJumped = true;

        }

        else if (Input.GetKeyDown(KeyCode.Z) && hasDoubleJumped)
        {
            doubleJump = false;

        }

        if (Input.GetKeyDown(KeyCode.C)) {

            pressedDash = true;
            AudioManager.instance.PlaySound("Dash");
        }

        else {
            pressedDash = false;
        }


        if (!wasGrounded && isGrounded)
        {
            AudioManager.instance.PlaySound("Landing");
            impact.transform.position = footsteps.transform.position;
            impact.Play();

        }

        wasGrounded = isGrounded;

    }

    void FixedUpdate() //como estoy moviendo al jugador con las fuerzas del rigidbody, llamo a los metodos en el fixedupdate
    {
        PlayerMove();
        PlayerJump();

        PlayerDash();

    }

    //--------------------------------------------------------------------METODOS PROPIOS--------------------------------------------------------------------

    //--------------------------------------------------------------------EVENTS

    public static void OnPlayerDeath()
    {
        onPlayerDeath?.Invoke();
    }

    //public void OnAllEnemiesDeath()
    //{
    //    enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //    if (enemies.Length == 0) onAllEnemiesDeath?.Invoke();

    //}

    //--------------------------------------------------------------------PLAYER HEALTH Y SCORE

    public int GetPlayerLives()
    {
        onPlayerLivesChange?.Invoke(GameManager.playerLives);
        return GameManager.playerLives;
    }

    public int GetPlayerStars()
    {
        onPlayerStarsChange?.Invoke(GameManager.playerStars);
        return GameManager.playerStars;
    }

    public int PlayerStarsUp()
    { 
        onPlayerStarsChange?.Invoke(GetPlayerStars());
        return GameManager.playerStars++;
    }

    public int PlayerLivesUp()
    { //parametro que indica la cantidad de vidas que gana 
        if (GetPlayerStars() >= 2) 
        onPlayerStarsChange?.Invoke(GetPlayerLives());
        return GameManager.playerLives++;
    }
    //--------------------------------------------------------------------MOVIMIENTO Y ATAQUE


    //Metodo para que el jugador se mueva con el input del usuario. 
    public void PlayerMove()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal"); //establecemos el eje horizontal con getaxis


        rbPlayer.velocity = new Vector3(ejeHorizontal * speedPlayer, rbPlayer.velocity.y, 0); //modifico la velocidad del jugador para poder moverlo a traves del input. en el ejehorizontal estara el input, en el vertical queda igual(10 unidades por segundo) y en el z es 0. esto es para poder moverlo unicamente en los ejes horizontal y vertical, y no en el eje z.

        transform.forward = new Vector3(ejeHorizontal, 0, (Mathf.Abs(ejeHorizontal) - 1)); //con este metodo transformo la direccion en la que mira el jugador. paso el input como parametro horizontal, 0 al vertical, y en el eje z saco el absoluto del horizontal (es decir el modulo) y le resto 1. 



        //determino si el jugador se mueve, y le asigno true a la variable booleana para correr
        if (ejeHorizontal != 0)
        {
            if (isGrounded) AudioManager.instance.PlaySound("Steps");
            else AudioManager.instance.StopSound("Steps");
            animPlayer.SetBool("isRunning", true);
            if (isGrounded) footsteps.Play(); //disparo las particulas
        }

        else
        {
            AudioManager.instance.StopSound("Steps");
            animPlayer.SetBool("isRunning", false);
        }

    }

    [SerializeField] private float fallMultiplier;
    private bool hasDoubleJumped;
    private bool doubleJump;
    //declaro variables tanto para el salto en el contexto normal como en el espejo.
    Vector3 jump = Vector3.up; //vector up es 1 en el eje y
    Vector3 jumpMirror = Vector3.down; //vector down es -1 en el eje y
                                       //metodo para que el jugador salte al apretar Z
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private void PlayerJump()
    {


        if (pressedJump || doubleJump) //si pressedJump es true
        {
            animPlayer.SetTrigger(doubleJump ? "isDoubleJumping" : "isJumping"); //animacion para que el jugador salte
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


        pressedJump = false;
        doubleJump = false;


        if (mirror)
        {
            if (rbPlayer.velocity.y < 0 && !Input.GetKey(KeyCode.Z)) rbPlayer.AddForce(-fallMultiplier * gravedadMirror.y * jumpMirror, ForceMode.Acceleration);
        }

        else

        {
            if (rbPlayer.velocity.y > 0 && !Input.GetKey(KeyCode.Z)) rbPlayer.AddForce(fallMultiplier * gravedad.y * jump, ForceMode.Acceleration);
        }
    }

    private bool dashed;
    private bool pressedDash;
    private float dashCooldownGround = 0.3f;
    private float dashCooldownAir = 2.5f;
    private float timeGround;
    private float timeAir;

    private float dashVelocity = 550f;

    private void PlayerDash()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal"); //establecemos el eje horizontal con getaxis
        float ejeVertical = Input.GetAxis("Vertical"); //establecemos el eje horizontal con getaxis

        if (pressedDash && !dashed)
        {
            dashEffect.transform.position = footsteps.transform.position;
            dashEffect.Play();
            Debug.Log("estoy dashing");
            rbPlayer.AddForce(new Vector3(ejeHorizontal * 5f, 0, 0) * dashVelocity, ForceMode.Impulse);
            dashed = true;
        }

        var dashedAndGrounded =  (dashed && isGrounded) ? timeGround += Time.deltaTime : timeAir += Time.deltaTime;
        

        if (timeGround >= dashCooldownGround)
        {
            timeGround = 0f;
            dashed = false;
        }

         if (timeAir >= dashCooldownAir)
        {
            dashed = false;
            timeAir = 0f;
        }
    }


    private bool attacked;
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.instance.PlaySound("Attack");
            animPlayer.SetBool("isAttacking", true);
            attacked = true;
            attackEffect.Play();
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            animPlayer.SetBool("isAttacking", false);
            attacked = false;
        }
    }

    //--------------------------------------------------------------------GRAVEDAD

    public void SetGravity(Vector3 newGravity)
    {
        Physics.gravity = newGravity;
    }
    public Vector3 GetGravity()
    {
        Vector3 gravityActual = Physics.gravity;
        return gravityActual;
    }

    //--------------------------------------------------------------------ESPEJO

    //metodo al atravesar el espejo
    private void enterUpsideDown()
    {
        Debug.Log("Cruce el espejo");

        
        playerMesh.transform.Rotate(new Vector3(0, 0, -180f), Space.Self); //roto y bajo el personaje para que este alineado con el piso
        
        playerMesh.transform.position += new Vector3(0, 2.69f, 0);
        footsteps.transform.position = playerMesh.transform.position + new Vector3(0, -0.24f, 0);

        //cameras[0].SetActive(false); //cambio las camaras
        //cameras[1].SetActive(true);

        Physics.gravity = gravedadMirror; //invierto la gravedad
        mirror = true;


        foreach (GameObject enemy in enemiesFloor) //convierto el uso de gravedad de los enemigos en el piso a falso, asi no se caen :) 
        {
            //primero chequeo que el enemigo exista sino me pincha todo
            if (enemy != null) enemy.GetComponent<Rigidbody>().useGravity = false;
            else Debug.Log("Ya mataste a este enemigo");

        }

        foreach (GameObject enemy in enemiesCeiling) //convierto el uso de gravedad de los enemigos en el techo a verdadero
        {

            if (enemy != null) enemy.GetComponent<Rigidbody>().useGravity = true;
            else Debug.Log("Ya mataste a este enemigo");

        }

    }

    //basicamente lo mismo pero al reves
    private void outOfUpsideDown()
    {
        Debug.Log("Volvi a la normalidad");

        playerMesh.transform.Rotate(new Vector3(0, 180f, 180f), Space.World);
        playerMesh.transform.position += new Vector3(0, -2.69f, 0);
        footsteps.transform.position = playerMesh.transform.position + new Vector3(0, 0.24f, 0);

        //cameras[1].SetActive(false); ahora estoy usando un unity event para manejar esto
        //cameras[0].SetActive(true);

        Physics.gravity = gravedad;
        mirror = false;

        foreach (GameObject enemy in enemiesFloor)
        {
            if (enemy != null) enemy.GetComponent<Rigidbody>().useGravity = true;

            else Debug.Log("Ya mataste a este enemigo");
        }

        foreach (GameObject enemy in enemiesCeiling)
        {
            if (enemy != null) enemy.GetComponent<Rigidbody>().useGravity = false;

            else Debug.Log("Ya mataste a este enemigo");
        }
    }

    private bool isHit = false;

    //--------------------------------------------------------------------COLISIONES--------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject;
            if (animPlayer.GetBool("isAttacking") == false)
            {
                animPlayer.SetTrigger("isAttacked");
                foreach (GameObject camera in cameras) if (camera.activeInHierarchy) StartCoroutine(camera.GetComponent<CameraShake>().Shake(0.1f, -4f));

                // rbPlayer.AddForce((transform.position-enemy.transform.position).normalized * 200f, ForceMode.Impulse);
                GameManager.playerLives--;
                onPlayerLivesChange?.Invoke(GetPlayerLives());
                //globalPostProcessing.GetComponent<PostProcessingGlobalController>().colorEffect(true);
                Debug.Log("La cantidad de vidas es: " + GameManager.instance.GetPlayerLives());
            }

            else if (animPlayer.GetBool("isAttacking") == true)

            {
                enemy.GetComponent<Enemy>().EnemyLivesDown();
                enemy.GetComponentInChildren<Animator>().SetTrigger("isAttacked");
                Debug.Log("Al enemigo le quedan: " + enemy.GetComponent<Enemy>().GetEnemyLives());
            
            }
            
        }

        if (collision.gameObject.CompareTag("Trap") && !isHit)
        {
            AudioManager.instance.PlaySound("Thump");
            isHit = true;
            animPlayer.SetTrigger("isHit");
            onPlayerFallen?.Invoke();
            GameManager.playerLives--;
            onPlayerLivesChange?.Invoke(GetPlayerLives());
            Debug.Log("La cantidad de vidas es: " + GetPlayerLives());
        }

    }

    private void OnCollisionStay(Collision collision) 
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject;
            if (animPlayer.GetBool("isAttacking") == true)

            {
                enemy.GetComponent<Enemy>().EnemyLivesDown();
                enemy.GetComponentInChildren<Animator>().SetTrigger("isAttacked");
                Debug.Log("Al enemigo le quedan: " + enemy.GetComponent<Enemy>().GetEnemyLives());
            }
        }

        if ((collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Platform")) && !isHit) 
        {
            Debug.Log("Estoy tocando el piso");
            isGrounded = true;
            animPlayer.SetBool("isGrounded", true);
            if (collision.gameObject.CompareTag("Floor")) lastGrounded = transform.position;

        }

        }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) //detecto si deje de estar en el piso para no poder saltar 
        {
            Debug.Log("NO estoy tocando el piso");
            isGrounded = false;
            animPlayer.SetBool("isGrounded", false);
        }
    }

    //--------------------------------------------------------------------TRIGGERS--------------------------------------------------------------------


    public void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    var enemy = other.gameObject;
        //    if (animPlayer.GetBool("isAttacking") == true)

        //    {
        //        enemy.GetComponent<Enemy>().EnemyLivesDown();
        //        enemy.GetComponentInChildren<Animator>().SetTrigger("isAttacked");
        //        Debug.Log("Al enemigo le quedan: " + enemy.GetComponent<Enemy>().GetEnemyLives());
        //    }
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    var enemy = other.gameObject;
        //    if (animPlayer.GetBool("isAttacking") == true)

        //    {
        //        enemy.GetComponent<Enemy>().EnemyLivesDown();
        //        enemy.GetComponentInChildren<Animator>().SetTrigger("isAttacked");
        //        Debug.Log("Al enemigo le quedan: " + enemy.GetComponent<Enemy>().GetEnemyLives());
        //    }
        //}

        if (other.gameObject.CompareTag("Star")) //si toco las estrellas las "agarro" 
        {
            AudioManager.instance.PlaySound("Star");
            PlayerStarsUp();
            Debug.Log("Tienes " + GetPlayerStars() + " estrellas");
            other.gameObject.SetActive(false);
        }

        

        if (other.gameObject.CompareTag("SpecialStar")) 
        {
            onLevelChange?.Invoke();
        }

        //atravieso el espejo y paso al techo
        else if (other.gameObject.CompareTag("Mirror") && !mirror) 
        {
            AudioManager.instance.PlaySound("Mirror");
            enterUpsideDown();
            other.gameObject.GetComponent<MirrorRotation>().Rotation();
            
        }

        //vuelvo a atravesar el espejo y retorno a la normalidad
        else if (other.gameObject.CompareTag("Mirror") && mirror)
        {
            AudioManager.instance.PlaySound("Mirror");
            outOfUpsideDown();
            other.gameObject.GetComponent<MirrorRotation>().Rotation();
            
        }

         if ((other.CompareTag("Void") || other.CompareTag("Obstacle")) && !isHit)
        {
            isHit = true;
            onPlayerFallen?.Invoke();
            GameManager.playerLives--;
            onPlayerLivesChange?.Invoke(GetPlayerLives());
            Debug.Log("La cantidad de vidas es: " + GetPlayerLives());
        }
        
    }


    public void ResetPosition()
    {
        StartCoroutine(ResetPositionCorrutina());
    }
    public IEnumerator ResetPositionCorrutina()
    {
        float elapsed = 0f;

        while (elapsed < 2.3f)
        {
            lastPostion = transform.position.x;
            elapsed += Time.deltaTime;
            yield return null;

        }

        // GetComponent<Collider>().enabled = true;
        if (lastPostion < lastGrounded.x ) transform.position = lastGrounded + groundedOffset;
        else transform.position = lastGrounded - groundedOffset;
        isHit = false;
    }

    public IEnumerator DelayHits()
    {
        float elapsed = 0f;
        float delay = 4f;

        while (elapsed < delay)
        {
            isHit = true;
            lastPostion = transform.position.x;
            elapsed += Time.deltaTime;
            yield return null;

        }

        isHit = false;
    }

}


