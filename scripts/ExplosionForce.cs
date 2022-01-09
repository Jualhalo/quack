using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosionForce : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float life = 0.5f;
    public AudioClip explosion;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.PlayOneShot(explosion, 3.0f);
        Vector3 explosionPos = transform.position;

        //kerätään taulukkoon kaikki colliderit johon OverlapSphere osuu
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        /*tutkitaan jokaista taulukkoon kerättyä collideria ja jos ollaan osuttu
         objektiin, jolla on rigidbody niin lisätään siihen explosionforcea
        */
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            NavMeshAgent nm = hit.GetComponent<NavMeshAgent>();
            Enemy_Health enemyhp = hit.GetComponent<Enemy_Health>();

            if (rb != null)
            {
                if (enemyhp != null)
                {
                    enemyhp.TakeDamage(100);
                }
                rb.AddExplosionForce(power, explosionPos, radius, 0.0f, ForceMode.Impulse);
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
