using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExplosions : MonoBehaviour
{
    public ParticleSystem nuke1;
    public ParticleSystem nuke2;
    public ParticleSystem nuke3;
    public ParticleSystem nuke4;
    public ParticleSystem nuke5;
    public ParticleSystem nuke6;
    public ParticleSystem nuke7;

    private float explosionTimer = 3.0f;
    private float lastExplosion;
    private int explosionNumber;

    void OnEnable()
    {
        explosionTimer = Random.Range(1.0f, 3.0f);
        explosionNumber = Random.Range(1, 8);
        if (explosionNumber == 1)
        {
            nuke1.Play();
        }
        if (explosionNumber == 2)
        {
            nuke2.Play();
        }
        if (explosionNumber == 3)
        {
            nuke3.Play();
        }
        if (explosionNumber == 4)
        {
            nuke4.Play();
        }
        if (explosionNumber == 5)
        {
            nuke5.Play();
        }
        if (explosionNumber == 6)
        {
            nuke6.Play();
        }
        if (explosionNumber == 7)
        {
            nuke7.Play();
        }
    }

    void Update()
    {
        if (Time.time > lastExplosion + explosionTimer)
        {
            lastExplosion = Time.time;
            explosionTimer = Random.Range(1.0f, 3.0f);
            explosionNumber = Random.Range(1, 8);
            if (explosionNumber == 1)
            {
                nuke1.Play();
            }
            if (explosionNumber == 2)
            {
                nuke2.Play();
            }
            if (explosionNumber == 3)
            {
                nuke3.Play();
            }
            if (explosionNumber == 4)
            {
                nuke4.Play();
            }
            if (explosionNumber == 5)
            {
                nuke5.Play();
            }
            if (explosionNumber == 6)
            {
                nuke6.Play();
            }
            if (explosionNumber == 7)
            {
                nuke7.Play();
            }
        }
    }
}
