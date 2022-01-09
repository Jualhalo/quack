using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    public float speed = 20.0f;
    public float life = 5.0f;
    public GameObject explosionPrefab;

    void Start()
    {
        
        Invoke("Kill", life);
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            Kill();
        }
        else if (col.gameObject.tag == "Bullet")
        {
            Physics.IgnoreCollision(col.GetComponent<Collider>(), col);
        }

        else if (col.gameObject.tag == "Enemy")
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
            Kill();
        }
        
    }

    void Kill()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
