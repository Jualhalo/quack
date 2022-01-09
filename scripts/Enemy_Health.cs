using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    public int health = 120;
    public int maxhealth = 120;

    void OnEnable()
    {
        enemyMaster = GetComponent<Enemy_Master>();
        enemyMaster.EventEnemyTakeDamage += TakeDamage;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyTakeDamage -= TakeDamage;
    }



    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            enemyMaster.CallEventEnemyDie();
            enemyMaster.isAlive = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
