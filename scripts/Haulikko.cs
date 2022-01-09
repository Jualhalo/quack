using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Haulikko : MonoBehaviour
{
    private PlayerController player;
    private Transform gun;
    public GameObject gun2model;
    private Transform camera;
    public Transform haulikkoEnd;
    public GameObject hitPrefab;
    public GameObject bloodPrefab;
    public ParticleSystem muzzle;
    public ParticleSystem muzzle2;
    //public GameObject muzzlePrefab;
    private float range = 100;
    private Vector3 startPosition;
    public GameObject haulikkoAnim;
    public GameObject haulikko2Anim;
    //public Animation anim;
    private float reloadTime = 1.0f;
    private float animationTime = 0.4f;
    private float lastFireTime;
    public int ammo;
    public int ammoMax = 100;
    public Text ammoText;
    public bool dualwielding;
    private bool gun1 = true;
    private bool gun2;
    private Player_audio shotgunaudio;

    public GameObject gameManager;

    void OnEnable()
    {
        shotgunaudio = GetComponentInParent<Player_audio>();
        gun = transform;
        camera = gun.parent;
        AmmoUI();
        gun2model.SetActive(false);
    }

    void Update()
    {
        if (dualwielding)
        {
            gun2model.SetActive(true);
            reloadTime = 0.5f;
        }

        AmmoUI();
        if (ammo > 0 && Input.GetButton("Fire1") && Time.time > lastFireTime + reloadTime && Time.timeScale == 1)
        {           
            shotgunaudio.CallEventAudio_shotgun();

            lastFireTime = Time.time;
            GetAmmo(-1);
            AmmoUI();

            if (gun1)
            {
                muzzle.Play();
            }

            if (gun2)
            {
                muzzle2.Play();
            }

            Invoke("PlayAnimation", animationTime);

            //anim.Play("Reload");
            //GameObject muzzle = (GameObject)Instantiate(muzzlePrefab, haulikkoEnd.position, Quaternion.LookRotation(haulikkoEnd.forward));
            //muzzle.transform.parent = transform;

            for (int i = 0; i < 30; i++)
            {
                HaulikkoRaycast();
            }

            if (dualwielding)
            {
                SwitchGun();
            }
        }

        if (ammo > ammoMax)
        {
            ammo = ammoMax;
            AmmoUI();
        }

    }

    void HaulikkoRaycast()
    {
        
        RaycastHit hit;
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

        if (Physics.Raycast(camera.TransformPoint(startPosition), camera.forward + offset, out hit, range))
        {
            Enemy_Health enemyhp = hit.collider.GetComponent<Enemy_Health>();

            if (enemyhp != null)
            {
                enemyhp.TakeDamage(10);
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

    void PlayAnimation()
    {
        
        if (dualwielding)
        {
            if (gun2)
            {
                haulikkoAnim.GetComponent<Haulikko_Anim>().PlayAnimation();
            }
            if (gun1)
            {
                haulikko2Anim.GetComponent<Haulikko_Anim>().PlayAnimation();
            }
        }
        else
        {
            haulikkoAnim.GetComponent<Haulikko_Anim>().PlayAnimation();
        }

    }

    void SwitchGun()
    {
        gun1 = !gun1;
        gun2 = !gun2;
    }
}
