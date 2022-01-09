using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_audio : MonoBehaviour
{
    private AudioSource audio;

    public delegate void GeneralEventHandler();

    public event GeneralEventHandler Audio_healthpickup;
    public event GeneralEventHandler Audio_ammopickup;
    public event GeneralEventHandler Audio_shotgun;
    public event GeneralEventHandler Audio_machinegun;
    public event GeneralEventHandler Audio_sinko;
    public event GeneralEventHandler Audio_barrier;
    public event GeneralEventHandler Audio_damage;

    public AudioClip healthpickup;
    public AudioClip ammopickup;
    public AudioClip shotgunfire;
    public AudioClip machinegunfire;
    public AudioClip sinkofire;
    public AudioClip barrier;
    public AudioClip damage;

    void OnEnable()
    {
        audio = GetComponent<AudioSource>();
    }

    public void CallEventAudio_healthpickup()
    {
        audio.pitch = 1.0f;
        audio.PlayOneShot(healthpickup, 2.0f);
    }

    public void CallEventAudio_ammopickup()
    {
        audio.pitch = 1.0f;
        audio.PlayOneShot(ammopickup, 0.7f);
    }
    public void CallEventAudio_shotgun()
    {
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.PlayOneShot(shotgunfire, 1.0f);
    }
    public void CallEventAudio_machinegun()
    {
        audio.pitch = Random.Range(0.9f, 1.1f);
        audio.PlayOneShot(machinegunfire, 0.8f);
    }
    public void CallEventAudio_sinko()
    {
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.PlayOneShot(sinkofire, 0.65f);
    }
    public void CallEventAudio_barrier()
    {
        audio.pitch = 1.0f;
        audio.PlayOneShot(barrier, 0.25f);
    }

    public void CallEventAudio_damage()
    {
        audio.pitch = 1.0f;
        audio.PlayOneShot(damage, 0.5f);
    }
}
