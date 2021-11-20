using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{


    [SerializeField] Transform[] waypoints;
    [SerializeField] float minDistance = 0.2f;
    [SerializeField] float speedPlatform = 2f;

    private int currentIndex = 0;
    private bool goBack = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        transform.position += direction * speedPlatform * Time.deltaTime;

        float distance = deltaVector.magnitude;

        if (distance < minDistance)
        {
            if (currentIndex >= waypoints.Length - 1)
            {
                goBack = true;
                
            }

            else if (currentIndex <= 0)
            {
                goBack = false;
            }

            if (!goBack)
            {
                currentIndex++;
                
            }

            else if (goBack)
            {
                currentIndex--;
            }
                
        }



    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Estoy en la plataforma");
            
            Move();
            
        }
    }
}
