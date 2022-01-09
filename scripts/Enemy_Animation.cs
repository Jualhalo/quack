using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour
{
    //hyödynnetty tutoriaalia: https://www.youtube.com/watch?v=AcoYGGSPkWM&feature=youtu.be

    private Enemy_Master enemyMaster;
    private Animator enemyAnimator;

    void OnEnable()
    {
        enemyMaster = GetComponent<Enemy_Master>();

        if (GetComponent<Animator>() != null)
        {
            enemyAnimator = GetComponent<Animator>();
        }

        enemyMaster.EventEnemyDie += SetAnimationToDie;
        enemyMaster.EventEnemyMoving += SetAnimationToWalk;
        enemyMaster.EventEnemyReachedNavTarget += SetAnimationToShootingStance;
        enemyMaster.EventEnemyShootTarget += SetAnimationToShoot;
    }
    /*
    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableAnimator;
        enemyMaster.EventEnemyMoving -= SetAnimationToWalk;
        enemyMaster.EventEnemyReachedNavTarget -= SetAnimationToShootingStance;
        enemyMaster.EventEnemyShootTarget -= SetAnimationToShoot;
    }*/

    void SetAnimationToIdle()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isShootingStance", false);
            }
        }
    }

    void SetAnimationToWalk()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetBool("isMoving",true);
                enemyAnimator.SetBool("isShootingStance", false);
            }
        }
    }
    
    void SetAnimationToShootingStance()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.ResetTrigger("shoot");
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isShootingStance", true);
            }
        }
    }

    void SetAnimationToShoot()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isShootingStance", false);
                enemyAnimator.SetTrigger("shoot");
            }
        }
    }

    void SetAnimationToDie()
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.enabled)
            {
                enemyAnimator.ResetTrigger("shoot");
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isShootingStance", false);
                enemyAnimator.SetTrigger("die");
            }
        }
    }
    /*
    void DisableAnimator()
    {
        if (enemyAnimator != null)
        {
            enemyAnimator.enabled = false;
        }
    }*/
}
