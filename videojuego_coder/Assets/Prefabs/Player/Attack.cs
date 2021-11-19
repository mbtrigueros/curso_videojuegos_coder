using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private ParticleSystem attack;
    private List<ParticleCollisionEvent> particleCollisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<ParticleSystem>();
        particleCollisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(attack, other, particleCollisionEvents);

        for(int i = 0; i <particleCollisionEvents.Count; i++)
        {
            var Collider = particleCollisionEvents[i].colliderComponent;
            if (Collider.CompareTag("Enemy"))
            {
                GameManager.enemyLives--;
                Debug.Log("Al enemigo le quedan: " + GameManager.enemyLives);
                
            }


        }
    }
}
