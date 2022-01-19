using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Enemy
{
    private float durationAttack = 2f;
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
        else Walk();

    }

    public void Attack()
    {
        animEnemy.SetTrigger("Attack");
        StartCoroutine(TimeAttack());

    }

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
