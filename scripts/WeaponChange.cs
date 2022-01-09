using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public bool weapon1;
    public bool weapon2;
    public bool weapon3;
    public int weaponNumber = 1;
    public int weaponMax = 1;
    public GameObject weapon1model;
    public GameObject weapon2model;
    public GameObject weapon3model;
    public GameObject weapon2_gun1_muzzle;
    public GameObject weapon2_gun2_muzzle;

    public bool hasSinko;
    public bool hasShotgun;

    void Awake()
    {
        weapon1 = true;
        weapon2model.SetActive(false);
        weapon3model.SetActive(false);
    }


    void Update()
    {

        var d = Input.GetAxis("Mouse ScrollWheel");

        if (d > 0f)
        {
            weaponNumber++;
        }

        if (d < 0f)
        {
            weaponNumber--;
        }


        if (weaponNumber > weaponMax)
        {
            weaponNumber = 1;
        }
        if (weaponNumber < 1)
        {
            weaponNumber = weaponMax;
        }

        if (weaponNumber == 1)
        {
            ChangeWeapon1();
        }

        if (weaponNumber == 3 && hasSinko)
        {
            ChangeWeapon3();
        }

        if (weaponNumber == 2 && hasShotgun)
        {
            ChangeWeapon2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponNumber = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasShotgun)
        {
            weaponNumber = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && hasSinko)
        {
            weaponNumber = 3;
        }
    }

    void ChangeWeapon1()
    {
        weapon2 = false;
        weapon1 = true;
        weapon3 = false;
        weapon2model.SetActive(false);
        weapon1model.SetActive(true);
        weapon3model.SetActive(false);
        weapon2_gun1_muzzle.SetActive(false);
        weapon2_gun2_muzzle.SetActive(false);
    }
    void ChangeWeapon2()
    {
        weapon1 = false;
        weapon2 = true;
        weapon3 = false;
        weapon1model.SetActive(false);
        weapon2model.SetActive(true);
        weapon3model.SetActive(false);
        weapon2_gun1_muzzle.SetActive(false);
        weapon2_gun2_muzzle.SetActive(false);
    }
    void ChangeWeapon3()
    {
        weapon1 = false;
        weapon2 = false;
        weapon3 = true;
        weapon1model.SetActive(false);
        weapon2model.SetActive(false);
        weapon3model.SetActive(true);
        weapon2_gun1_muzzle.SetActive(false);
        weapon2_gun2_muzzle.SetActive(false);
    }
}
