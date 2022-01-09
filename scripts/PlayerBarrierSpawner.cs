using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrierSpawner : MonoBehaviour
{
    public GameObject barrierPrefab;
    public Transform barrierSpawnPoint;
    private float barrierUsed = 0.0f;
    public float barrierCooldownTime = 2.0f;
    private Player_audio playerAudio;

    void OnEnable()
    {
        playerAudio = GetComponentInParent<Player_audio>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire3") && Time.time > barrierUsed + barrierCooldownTime && Time.timeScale == 1)
        {
            playerAudio.CallEventAudio_barrier();
            GameObject go = (GameObject)Instantiate(barrierPrefab, barrierSpawnPoint.position, Quaternion.LookRotation(barrierSpawnPoint.forward));
            barrierUsed = Time.time;
        }
    }
}
