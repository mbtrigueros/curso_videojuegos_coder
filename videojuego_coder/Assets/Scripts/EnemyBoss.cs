using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{

    Sound explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        explosionSound = AudioManager.instance.GetSound("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        MoveForwards();
    }

    void MoveForwards()
    {
        AudioManager.instance.PlaySound("BossSteps");
        animEnemy.SetBool("bossAttack", true); //determino la variable como cierta para activar la animacion de correr
        transform.forward = Vector3.right;
        transform.position += enemyData.SpeedRunEnemy * transform.forward * Time.deltaTime;
    }

    ParticleSystem particulas;

    override public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject destroyable = collision.gameObject;

            if (destroyable.GetComponentInParent<ParticleSystem>() != null)
            {
                particulas = destroyable.GetComponentInParent<ParticleSystem>();
                destroyable.SetActive(false);

                if (!explosionSound.source.isPlaying)
                    AudioManager.instance.PlaySound("Explosion");
                particulas.Play();
            }
            else destroyable.SetActive(false);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject destroyable = collision.gameObject;
            if (destroyable.GetComponentInParent<ParticleSystem>() != null)
            {
                if (collision.gameObject.CompareTag("Floor") && !canDestroy)
                {
                    StartCoroutine(DelayDestroy());
                    particulas = destroyable.GetComponentInParent<ParticleSystem>();
                    destroyable.SetActive(false);
                    particulas.Play();
                    canDestroy = false;
                }

                else destroyable.SetActive(false);
            }

        }
    }

    bool canDestroy;

    private IEnumerator DelayDestroy()
    {
        float elapsed = 0f;
        float waitTime = 8f;

        while (elapsed < waitTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        canDestroy = true;
    }

}
