using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{

    [SerializeField] private float cooldown = 0.5f; //tiempo de descanso entre cada disparo
    [SerializeField] private float timePassed = 0f;

    [SerializeField] private LayerMask playerMask;

    private bool playerDetected = false;

    private Rigidbody rbTrap;

    [SerializeField] private float speedFall = 12f;

    // Start is called before the first frame update
    void Start()
    
    {
        playerDetected = false;
        rbTrap = GetComponent<Rigidbody>();
        rbTrap.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, playerMask))
        {
            playerDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            rbTrap.useGravity = true;

        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.blue);

        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Has muerto!!!!!!!!!!!!");
            //Destroy(collision.gameObject);
        }
    }

}
