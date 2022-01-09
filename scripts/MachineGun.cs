using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : MonoBehaviour
{
    private PlayerController player;
    private Transform gun;
    public GameObject gun2model;
    private Transform camera;
    //public GameObject gun1muzzle;
    //public GameObject gun2muzzle;
    public GameObject hitPrefab;
    public GameObject bloodPrefab;
    private float range = 400;
    private Vector3 startPosition;
    private float reloadTime = 0.1f;
    private float lastFireTime;
    public int ammo;
    public int ammoMax = 600;
    public Text ammoText;

    public bool dualwielding;

    public ParticleSystem gun1muzzle;
    public ParticleSystem gun2muzzle;

    private Player_audio machinegunaudio;

    void OnEnable()
    {
        machinegunaudio = GetComponentInParent<Player_audio>();
        gun2model.SetActive(false);
        gun = transform;
        camera = gun.parent;
        AmmoUI();
    }


    void Update()
    {        
        if (dualwielding)
        {
            gun2model.SetActive(true);
        }

        AmmoUI();
        if (ammo > 0 && Input.GetButton("Fire1") && Time.time > lastFireTime + reloadTime && Time.timeScale == 1)
        {
            
            gun1muzzle.Play();
            if (dualwielding && ammo >= 2)
            {
                gun2muzzle.Play();
            }          

            lastFireTime = Time.time;

            /*jos pelaaja on löytänyt toisen ak47n, ammutaan toisen kerran pienellä viiveellä,
             jos ammuksia on kuitenkin vähemmän kuin kaksi niin ammutaan vain toisesta aseesta*/
            if (dualwielding && ammo >= 2)
            {
                Invoke("MachineGunRaycast", 0.05f);
                /*
                for (int i = 0; i < 2; i++)
                {
                    MachineGunRaycast();
                }*/
            }
                MachineGunRaycast();
        }

        if (ammo > ammoMax)
        {
            ammo = ammoMax;
            AmmoUI();
        }

    }

    void MachineGunRaycast()
    {
        machinegunaudio.CallEventAudio_machinegun();

        GetAmmo(-1);
        AmmoUI();

        RaycastHit hit;
        Vector3 offset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));

        if (Physics.Raycast(camera.TransformPoint(startPosition), camera.forward + offset, out hit, range))
        {
            Enemy_Health enemyhp = hit.collider.GetComponent<Enemy_Health>();

            if (enemyhp != null)
            {
                enemyhp.TakeDamage(20);
                int bloodRotation = Random.Range(0, 2);
                if (bloodRotation == 1)
                {
                    GameObject go1 = (GameObject)Instantiate(bloodPrefab, hit.point, Quaternion.LookRotation(-hit.normal));
                }
                else
                {
                    GameObject go1 = (GameObject)Instantiate(bloodPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            else
            {
                GameObject go = (GameObject)Instantiate(hitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 10f);

            }
        }
    }

    public void AmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = ammo.ToString();
        }
    }

    public void GetAmmo(int amount)
    {
        ammo += amount;
    }
}
