using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float life = 1.0f;

    void Start()
    {
        Invoke("Kill", life);
    }


    public void Kill()
    {
        Destroy(gameObject);
    }
}
