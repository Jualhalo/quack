using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public LayerMask detectionLayer;
    private Transform monster;
    private Animator enemyAnimator;
    public GameObject projectilePrefab;
    public Transform monsterGunBarrel;
    private NavMeshAgent nm;
    //private Rigidbody rb;
    private Collider[] hitColliders;
    private float checkRate = 1.0f;
    private float nextCheck;
    private float detectionRadius = 50;
    private bool isMoving;
    private bool isDead;
    public float reloadTime = 0.5f;
    private float lastFireTime;
    public int health = 120;

    void Start()
    {
        monster = transform;
        nm = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        CheckIfPlayerInRange();
        CheckIfDestinationReached();

        if (isMoving)
        {
            SetAnimationToWalk();
            
        }

        if (health <= 0 && isDead == false)
        {
            Kill();
        }
    }

    void CheckIfPlayerInRange()
    {
        if (Time.time > nextCheck && nm.enabled)
        {
            nextCheck = Time.time + checkRate;
            hitColliders = Physics.OverlapSphere(monster.position, detectionRadius, detectionLayer);

            if (hitColliders.Length > 0 && nm.enabled)
            {
                nm.SetDestination(hitColliders[0].transform.position);
                isMoving = true;
            }

            else
            {
                isMoving = false;
            }
        }
    }

    void CheckIfDestinationReached()
    {
        if (Time.time > nextCheck && nm.enabled)
        {
            if (isMoving)
            {
                if (nm.remainingDistance < nm.stoppingDistance)
                {
                    isMoving = false;
                    //SetAnimationToAim();
                }
            }
        }
    }

    void Shoot()
    {
        if (Time.time > lastFireTime + reloadTime)
        {
            int shoot = Random.Range(0, 10);
            if (shoot == 1)
            {
                Vector3 offset = new Vector3(Random.Range(-0.05f,0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
                Vector3 xAxisSpread = new Vector3(0.1f,0,0);
                Vector3 yAxisSpread = new Vector3(0f, 0.1f, 0);
                GameObject go = (GameObject)Instantiate(projectilePrefab, monsterGunBarrel.position, Quaternion.LookRotation(monsterGunBarrel.forward + offset));
                //GameObject go1 = (GameObject)Instantiate(projectilePrefab, monsterGunBarrel.position, Quaternion.LookRotation(monsterGunBarrel.forward + offset + xAxisSpread));
                //GameObject go2 = (GameObject)Instantiate(projectilePrefab, monsterGunBarrel.position, Quaternion.LookRotation(monsterGunBarrel.forward + offset - xAxisSpread));
                //GameObject go3 = (GameObject)Instantiate(projectilePrefab, monsterGunBarrel.position, Quaternion.LookRotation(monsterGunBarrel.forward + offset + yAxisSpread));
                //GameObject go4 = (GameObject)Instantiate(projectilePrefab, monsterGunBarrel.position, Quaternion.LookRotation(monsterGunBarrel.forward + offset - yAxisSpread));
                lastFireTime = Time.time;
            }

        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    void Kill()
    {
        isDead = true;
        nm.enabled = false;
        //rb.isKinematic = false;
        isMoving = false;
        SetAnimationToDie();
        gameObject.GetComponent<Collider>().enabled = false;
    }

    void SetAnimationToIdle()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isShootingStance", false);
            }
        }
    }

    void SetAnimationToWalk()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetBool("isMoving", true);
            }
        }
    }

    void SetAnimationToAim()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetBool("isShootingStance", true);
            }
        }
    }

    void SetAnimationToShoot()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetTrigger("shoot");
            }
        }
    }

    void SetAnimationToDie()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetTrigger("die");
            }
        }
    }
}
