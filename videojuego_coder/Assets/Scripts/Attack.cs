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
        particleCollisionEvents = new List<ParticleCollisionEvent>(); //aca traigo todos los elementos con los que puede colisionar el sistema de particulas
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(attack, other, particleCollisionEvents);

        for(int i = 0; i <particleCollisionEvents.Count; i++) //recorro la lista de colisiones
        {
            var enem = particleCollisionEvents[i].colliderComponent; 
            if (enem.CompareTag("Enemy")) //comparo el elemento de la lista y verifico  si tiene la tag Enemy
            {
                enem.GetComponent<EnemyController>().enemyLives--; //le bajo la vida al Enemy
                Debug.Log("Al enemigo le quedan: " + enem.GetComponent<EnemyController>().enemyLives);
                
            }


        }
    }
}
