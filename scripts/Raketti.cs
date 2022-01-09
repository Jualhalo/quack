using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raketti : MonoBehaviour
{
    public float speed = 20.0f;
    public float life = 10.0f;
    public float rakettiLife = 5.0f;
    public GameObject explosionPrefab;
    public Transform raketti;
    private bool flying;
    private bool rakettiDead;


    void Start()
    {
        flying = true;
        raketti = gameObject.transform.GetChild(0);
        Invoke("Kill", life);
        Invoke("KillRaketti", rakettiLife);
    }

    void Update()
    {
        if (flying)
        {
            //pelaaja voi räjäyttää kaikki ampumansa raketit kesken lennon right-click
            if (Input.GetButtonDown("Fire2"))
            {
                KillRaketti();
            }
        }
 
    }

    void FixedUpdate()
    {
        if (flying)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
    }

    void OnTriggerEnter(Collider col)
    //void OnCollisionEnter (Collision c)
    {
        if (flying)
        {
            if (col.gameObject.tag == "Bullet")
            {
                Physics.IgnoreCollision(col.GetComponent<Collider>(), col);
            }
            else if (col.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(col.GetComponent<Collider>(), col);
            }
            else if (col.gameObject.tag == "WeaponPickUp_AK")
            {
                Physics.IgnoreCollision(col.GetComponent<Collider>(), col);

            }
            else if (col.gameObject.tag == "WeaponPickUp_Shotgun")
            {
                Physics.IgnoreCollision(col.GetComponent<Collider>(), col);

            }
            else if (col.gameObject.tag == "WeaponPickUp_Sinko")
            {
                Physics.IgnoreCollision(col.GetComponent<Collider>(), col);

            }
            else
            {
                KillRaketti();
            }
        }

    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void KillRaketti()
    {
        if (!rakettiDead)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(raketti.gameObject);
            flying = false;
            rakettiDead = true;
        }
    }
}
