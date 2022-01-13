using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Enemy
{
    private bool hasAttacked = false;
    protected Rigidbody rbEnemy; //rigidbody del enemigo
    // Start is called before the first frame update
    void Start()
    {
        rbEnemy = GetComponent<Rigidbody>(); //rigidbody enemigo
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDies();
        DetectPlayer(origen.transform);

        if (playerSeen && !hasAttacked)
        {
            Attack();
        }
        else
        {
            Walk();
        }
    }

    private void Attack()
    {
        animEnemy.SetTrigger("Attack");
        StartCoroutine(TimeAttack());

    }

    private float durationAttack = 2f;
    public IEnumerator TimeAttack()
    {
        float elapsed = 0f;

        while (elapsed < durationAttack)
        {

            hasAttacked = true;
            elapsed += Time.deltaTime;
            yield return null;

        }

        hasAttacked = false;
        playerSeen = false;
    }

}
