using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    private float healthMax = 100;
    public Text healthText;
    public Text itemText;
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float jumpheight = 2.0f;
    public float maxVelocityChange = 10.0f;
    private Rigidbody rb;
    private int jumpCount = 5;
    private float nextJump = 0.3f;

    public bool alive;
    private bool damaged;
    private bool reachedExit;
    
    public GameObject sinko;
    public GameObject machinegun;
    public GameObject haulikko;
    public GameObject barrierSpawn;
    public GameObject mouselook;

    public GameObject damageImage;
    public GameObject pickupImage;
    public float damageFlashSpeed = 0.3f;
    public float pickupFlashSpeed = 0.1f;

    private WeaponChange weaponChange;

    private Player_audio playerAudio;
    private GameManager_Master gameManagerMaster;

    void Awake()
    {
        playerAudio = GetComponent<Player_audio>();
        alive = true;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        HealthUI();
        weaponChange = GetComponent<WeaponChange>();
    }
    void FixedUpdate()
    {
        if (alive)
        {
            //tässä lasketaan liikkumisnopeus
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            //tässä lisätään forcee rigidbodyyn, jotta saavutetaan liikkumisnopeus
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            //hyppy, toimii vain jos pelaaja on kiinni maassa
            //toisin kuin alkuperäisessä koodissa if (grounded) sisältää vain hypyn
            //tällöin pelaajalla on täysi air control ja voi liikuttaa hahmoo hypätessä ja lentäessä
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector3(velocity.x, JumpSpeed(), velocity.z);
                //jumpCount--;
            }

            //tämä kohta lisää painovoiman manuaalisesti hienosäätöä varten
            rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

        }
       
    }

    void Update()
    {

        if (IsGrounded())
        {
            jumpCount = 1;
        }

        if (health <= 0 && alive)
        {
            GameOver();
        }

        if (health > healthMax)
        {
            health = healthMax;
        }

        HealthUI();
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (damage > 0)
        {
            playerAudio.CallEventAudio_damage();
            DamageEffect();
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    void OnTriggerEnter(Collider target)
    {
        if (alive && health < healthMax && target.tag == "HealthPickup")
        {
            takeDamage(-25);
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_healthpickup();

            StopCoroutine(ClearTextUI());
            ItemTextUI("PICKED UP A MEDKIT");
            StartCoroutine(ClearTextUI());
        }

        if (alive && haulikko.GetComponent<Haulikko>().ammo < haulikko.GetComponent<Haulikko>().ammoMax && target.tag == "ShotgunAmmo")
        {
            haulikko.GetComponent<Haulikko>().GetAmmo(10);
            haulikko.GetComponent<Haulikko>().AmmoUI();
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_ammopickup();

            StopCoroutine(ClearTextUI());
            ItemTextUI("PICKED UP SHOTGUN SHELLS");
            StartCoroutine(ClearTextUI());
        }

        if (alive && machinegun.GetComponent<MachineGun>().ammo < machinegun.GetComponent<MachineGun>().ammoMax && target.tag == "MgAmmo")
        {
            machinegun.GetComponent<MachineGun>().GetAmmo(50);
            machinegun.GetComponent<MachineGun>().AmmoUI();
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_ammopickup();

            StopCoroutine(ClearTextUI());
            ItemTextUI("PICKED UP RIFLE AMMO");
            StartCoroutine(ClearTextUI());
        }

        if (alive && sinko.GetComponent<Sinko>().ammo < sinko.GetComponent<Sinko>().ammoMax && target.tag == "SinkoAmmo")
        {
            sinko.GetComponent<Sinko>().GetAmmo(10);
            sinko.GetComponent<Sinko>().AmmoUI();
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_ammopickup();

            StopCoroutine(ClearTextUI());
            ItemTextUI("PICKED UP ROCKETS");
            StartCoroutine(ClearTextUI());
        }

        if (alive && target.tag == "WeaponPickUp_AK")
        {
            machinegun.GetComponent<MachineGun>().GetAmmo(50);
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_ammopickup();
            weaponChange.weaponNumber = 1;
            
            StopCoroutine(ClearTextUI());
            ItemTextUI("PICKED UP ANOTHER AK47");
            StartCoroutine(ClearTextUI());

            if (!machinegun.GetComponent<MachineGun>().dualwielding)
            {
                machinegun.GetComponent<MachineGun>().dualwielding = true;
                weaponChange.weaponNumber = 1;
            }
        }

        if (alive && target.tag == "WeaponPickUp_Shotgun")
        {
            haulikko.GetComponent<Haulikko>().GetAmmo(10);
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_ammopickup();

            if (weaponChange.hasShotgun)
            {               
                StopCoroutine(ClearTextUI());
                ItemTextUI("PICKED UP ANOTHER SHOTGUN");
                StartCoroutine(ClearTextUI());

                if (!haulikko.GetComponent<Haulikko>().dualwielding)
                {                    
                    haulikko.GetComponent<Haulikko>().dualwielding = true;
                    weaponChange.weaponNumber = 2;
                }
            }
            if (!weaponChange.hasShotgun)
            {                
                StopCoroutine(ClearTextUI());
                ItemTextUI("PICKED UP A SHOTGUN");
                StartCoroutine(ClearTextUI());
                weaponChange.hasShotgun = true;
                weaponChange.weaponMax++;
                weaponChange.weaponNumber = 2;
            }
        }

        if (alive && target.tag == "WeaponPickUp_Sinko")
        {
            sinko.GetComponent<Sinko>().GetAmmo(10);
            target.gameObject.SetActive(false);
            PickupEffect();
            playerAudio.CallEventAudio_ammopickup();

            if (weaponChange.hasSinko)
            {                
                StopCoroutine(ClearTextUI());
                ItemTextUI("PICKED UP ANOTHER ROCKET LAUNCHER");
                StartCoroutine(ClearTextUI());

                if (!sinko.GetComponent<Sinko>().dualwielding)
                {
                    sinko.GetComponent<Sinko>().dualwielding = true;
                    weaponChange.weaponNumber = 3;
                }
            }
            if (!weaponChange.hasSinko)
            {               
                StopCoroutine(ClearTextUI());
                ItemTextUI("PICKED UP A ROCKET LAUNCHER");
                StartCoroutine(ClearTextUI());
                weaponChange.hasSinko = true;
                weaponChange.weaponMax++;
                weaponChange.weaponNumber = 3;
            }
        }
        if (alive && !reachedExit && target.tag == "GOLL")
        {
            reachedExit = true;
            gameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
            gameManagerMaster.CallEventGameWonEvent();
        }
    }

    float JumpSpeed()
    {
        return Mathf.Sqrt(2 * jumpheight * gravity);
    }

    void HealthUI()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    void ItemTextUI(string msg)
    {
        if (itemText != null)
        {
            itemText.text = msg;
        }
    }

    IEnumerator ClearTextUI()
    {
        yield return new WaitForSeconds(2);
        if (itemText != null)
        {
            itemText.text = "";
        }
    }

    void DamageEffect()
    {
        if (damageImage != null)
        {
            StopCoroutine("PickupEffect");
            StopCoroutine("DamageEffect");
            damageImage.SetActive(true);
            if (alive)
            {
                StartCoroutine(ResetDamageImage());
            }           
        }
    }

    IEnumerator ResetDamageImage()
    {
        yield return new WaitForSeconds(damageFlashSpeed);
        damageImage.SetActive(false);
    }

    void PickupEffect()
    {
        if (pickupImage != null)
        {
            StopCoroutine("PickupEffect");
            StopCoroutine("DamageEffect");
            pickupImage.SetActive(true);
            StartCoroutine(ResetPickupImage());
        }
    }

    IEnumerator ResetPickupImage()
    {
        yield return new WaitForSeconds(pickupFlashSpeed);
        pickupImage.SetActive(false);
    }

    void GameOver()
    {
        alive = false;
        health = 0;
        HealthUI();        
        rb.useGravity = true;
        rb.freezeRotation = false;

        sinko.SetActive(false);
        machinegun.SetActive(false);
        haulikko.SetActive(false);
        //sinko.GetComponent<Sinko>().enabled = false;
        barrierSpawn.GetComponent<PlayerBarrierSpawner>().enabled = false;
        mouselook.GetComponent<Mouselook>().enabled = false;
        //machinegun.GetComponent<MachineGun>().enabled = false;
        GetComponent<WeaponChange>().enabled = false;

        gameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
        gameManagerMaster.CallEventGameOverEvent();
    }
}
