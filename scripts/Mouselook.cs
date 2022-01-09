using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;
    private float xAxisClamp;

    private Transform player;

    void Awake()
    {
        //LockCursor();
        xAxisClamp = 0.0f;
        player = this.transform.parent.transform;
    }

    void Update()
    {
        CameraRotation();
    }
    /*
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }*/

    void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        //jos pelaaja on maassa, niin rajoitetaan mouselook ettei voida katsoa koko 360 astetta ympäri
        //jos pelaaja on ilmassa niin poistetaan rajoite, jotta pelaaja voi tehdä flippejä ilmassa
        if (player.GetComponentInParent<PlayerController>().IsGrounded())
        {
            if (xAxisClamp > 90.0f)
            {
                xAxisClamp = 90.0f;
                mouseY = 0.0f;
                ClampXAxisRotationToValue(270.0f);
            }
            else if (xAxisClamp < -90.0f)
            {
                xAxisClamp = -90.0f;
                mouseY = 0.0f;
                ClampXAxisRotationToValue(90.0f);
            }
        }

        transform.Rotate(Vector3.left * mouseY);
        player.Rotate(Vector3.up * mouseX);
    }

    void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
