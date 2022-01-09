using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBulletExplosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float life = 0.5f;

    void Start()
    {
        Vector3 explosionPos = transform.position;

        //kerätään taulukkoon kaikki colliderit johon räjähdys (sphere) osuu
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        /*tutkitaan jokaista taulukkoon kerättyä collideria ja jos ollaan osuttu
         objektiin, jolla on rigidbody niin lisätään siihen explosionforcea
        */
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            PlayerController player = hit.GetComponent<PlayerController>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 0.0F, ForceMode.Impulse);
                if (player != null)
                {
                    player.takeDamage(6);
                }
            }



        }

        //tuhotaan räjähdysobjekti pienen ajan kuluttua
        Invoke("Kill", life);
    }
    void Kill()
    {
        Destroy(gameObject);
    }
}

