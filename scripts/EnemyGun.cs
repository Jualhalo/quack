using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public Transform target;
    private Transform enemyGun;
    private float rotationSpeed = 100f;

    void OnEnable()
    {
        enemyGun = transform;
    }

    void FixedUpdate()
    {
        RotateTowards();
    }

    void RotateTowards()
    {
        /*
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        */
        Vector3 lookAtVector = new Vector3(target.position.x, target.position.y, target.position.z);
        enemyGun.LookAt(lookAtVector);
    }
}
