using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sinko : MonoBehaviour
{
    public GameObject gun2model;
    public GameObject rocketPrefab;
    //public GameObject rocketFire;
    public Transform rocketBarrel;
    public Transform rocketBarrel2;
    private float reloadTime = 0.5f;
    private float lastFireTime;
    public int ammo;
    public int ammoMax = 100;
    public Text ammoText;
    public bool dualwielding;
    private bool gun1 = true;
    private bool gun2;

    public ParticleSystem rocketFire;
    public ParticleSystem rocketFire2;

    private Player_audio sinkoaudio;

    void OnEnable()
    {
        sinkoaudio = GetComponentInParent<Player_audio>();
        AmmoUI();
        gun2model.SetActive(false);
    }

    void Update()
    {
        if (dualwielding)
        {
            reloadTime = 0.25f;
            gun2model.SetActive(true);
        }

        AmmoUI();
        if (ammo > 0 && Input.GetButton("Fire1") && Time.time > lastFireTime + reloadTime && Time.timeScale == 1)
        {
            
            sinkoaudio.CallEventAudio_sinko();
            lastFireTime = Time.time;
            GetAmmo(-1);
            AmmoUI();

            if (gun1)
            {
                GameObject go = (GameObject)Instantiate(rocketPrefab, rocketBarrel.position, Quaternion.LookRotation(rocketBarrel.forward));
                rocketFire.Play();
            }
            if (gun2)
            {
                GameObject go = (GameObject)Instantiate(rocketPrefab, rocketBarrel2.position, Quaternion.LookRotation(rocketBarrel2.forward));
                rocketFire2.Play();
            }

            if (dualwielding)
            {
                SwitchGun();
            }
            //GameObject go2 = (GameObject)Instantiate(rocketFire, rocketBarrel.position, Quaternion.LookRotation(rocketBarrel.forward));
            //go2.transform.parent = transform;
            //Physics.IgnoreCollision(GetComponent<Collider>(), go.GetComponent<Collider>());            
        }

        if (ammo > ammoMax)
        {
            ammo = ammoMax;
            AmmoUI();
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

    void SwitchGun()
    {
        gun1 = !gun1;
        gun2 = !gun2;
    }
}
