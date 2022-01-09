using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Master : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=s7-WqmD3gHk&feature=youtu.be

    public Transform target;
    public bool isMoving;
    public bool isNavPaused;
    public bool isAlive = true;

    public delegate void GeneralEventHandler();

    public event GeneralEventHandler EventEnemyDie;
    public event GeneralEventHandler EventEnemyMoving;
    public event GeneralEventHandler EventEnemyReachedNavTarget;
    public event GeneralEventHandler EventEnemyShootingStance;
    public event GeneralEventHandler EventEnemyShootTarget;
    public event GeneralEventHandler EventEnemyLostTarget;

    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler EventEnemyTakeDamage;

    public delegate void NavTargetEventHandler(Transform targetTransform);
    public event NavTargetEventHandler EventEnemySetNavTarget;

    private AudioSource audio;
    public AudioClip death1;
    public AudioClip death2;
    public AudioClip death3;
    public AudioClip death4;

    private int randomAudio;

    public void CallEventEnemyTakeDamage(int health)
    {
        if (EventEnemyTakeDamage != null)
        {
            EventEnemyTakeDamage(health);
        }
    }

    public void CallEventEnemySetNavTarget(Transform targTransform)
    {
        if (EventEnemySetNavTarget != null)
        {
            EventEnemySetNavTarget(targTransform);
        }

        target = targTransform;
    }

    public void CallEventEnemyDie()
    {
        if (EventEnemyDie != null)
        {       
            PlayDeathAudio();
            EventEnemyDie();
        }
    }

    public void CallEventEnemyMoving()
    {
        if (EventEnemyMoving != null)
        {
            EventEnemyMoving();
        }
    }

    public void CallEventEnemyReachedNavTarget()
    {
        if (EventEnemyReachedNavTarget != null)
        {
            EventEnemyReachedNavTarget();
        }
    }

    public void CallEventEnemyShootingStance()
    {
        if (EventEnemyShootingStance != null)
        {
            EventEnemyShootingStance();
        }
    }

    public void CallEventEnemyShootTarget()
    {
        if (EventEnemyShootTarget != null)
        {
            EventEnemyShootTarget();
        }
    }

    
    public void CallEventEnemyLostTarget()
    {
        if (EventEnemyLostTarget != null)
        {
            EventEnemyLostTarget();
        }

        target = null;
    }

    public void PlayDeathAudio()
    {
        audio = GetComponent<AudioSource>();
        randomAudio = Random.Range(1, 5);

        if (randomAudio == 1)
        {
            audio.pitch = Random.Range(0.5f, 1.2f);
            audio.PlayOneShot(death1, 1.5f);
        }
        if (randomAudio == 2)
        {
            audio.pitch = Random.Range(0.9f, 1.1f);
            audio.PlayOneShot(death2, 1.5f);
        }
        if (randomAudio == 3)
        {
            audio.pitch = Random.Range(0.7f, 0.8f);
            audio.PlayOneShot(death3, 1.5f);
        }
        if (randomAudio == 4)
        {
            audio.pitch = Random.Range(0.7f, 1.2f);
            audio.PlayOneShot(death4, 2.0f);
        }
    }
}
