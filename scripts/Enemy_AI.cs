using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    /*Tässä scriptissä on hyödynnetty tutoriaaleja:
     https://www.youtube.com/watch?v=CxEBUCB31Aw&feature=youtu.be
     https://www.youtube.com/watch?v=UJ-2WuTunkE&feature=youtu.be
     https://www.youtube.com/watch?v=Pa5w9kkTU8g&feature=youtu.be 
     https://www.youtube.com/watch?v=rE83nHYj-eo&feature=youtu.be
     */

    private Enemy_Master enemyMaster;
    private Enemy_Health enemyHealth;
    private Transform enemyTransform;
    public Transform detector;
    private Transform shotTarget;
    private NavMeshAgent nm;
    private RaycastHit hit;
    public LayerMask playerLayer;
    public LayerMask sightLayer;
    private float checkRate;
    private float nextCheck;
    private float detectRadius = 50.0f;
    private float shootRate = 0.6f;
    private float nextShot;
    private float shotRange;

    private float changeState;
    private float nextChangeState;

    public GameObject projectilePrefab;
    public Transform enemyGunBarrel;

    private Transform target;

    private AudioSource audio;
    public AudioClip shot;

    void OnEnable()
    {
        audio = GetComponent<AudioSource>();
        enemyMaster = GetComponent<Enemy_Master>();
        enemyHealth = GetComponent<Enemy_Health>();
        enemyTransform = transform;
        nm = GetComponent<NavMeshAgent>();
        nm.stoppingDistance = Random.Range(50, 100);
        checkRate = Random.Range(0.8f, 1.2f);
        shotRange = nm.stoppingDistance;

        enemyMaster.EventEnemyDie += DisableThis;
        enemyMaster.EventEnemySetNavTarget += SetShotTarget;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
        nm.enabled = false;
        this.enabled = false;
    }

    void Update()
    {
        CarryOutDetection();
        TryToMoveTowardsTarget();
        CheckIfDestinationReached();
        TryToShoot();

        changeState = Random.Range(1.0f, 3.0f);

        if (Time.time > nextChangeState)
        {
            nextChangeState += Time.time + changeState;
            nm.stoppingDistance = Random.Range(50, 100);
            shotRange = nm.stoppingDistance;
        }

        //jos ollaan otettu damagee niin tiedetään heti asettaa pelaaja kohteeksi (vain pelaaja voi tehä damagee vihuille pelissä)
        if (enemyHealth.health < 120 && target == null)
        {
            target = GameObject.Find("Player").transform;
            enemyMaster.CallEventEnemySetNavTarget(target);
        }
    }

    void FixedUpdate()
    {
        if (shotTarget != null)
        {
            if (Vector3.Distance(enemyTransform.position, shotTarget.position) < 20)
            {
                Vector3 lookAtVector = new Vector3(shotTarget.position.x, enemyTransform.position.y, shotTarget.position.z);
                enemyTransform.LookAt(lookAtVector);
            }
                            
        }
    }

    void CarryOutDetection()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;

            //luodaan overlapsphere ja katsotaan osuuko pelaaja spheren sisälle
            Collider[] colliders = Physics.OverlapSphere(enemyTransform.position, detectRadius, playerLayer);

            if (colliders.Length > 0)
            {
                foreach (Collider potentialTargetCollider in colliders)
                {
                    if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                    {
                        if (potentialTargetCollider.CompareTag("Player"))
                        {
                            //jos pelaaja on spheren sisällä niin tarkistetaan voidaanko pelaaja nähdä
                            break;
                        }
                        
                    }

                }
            }
            
            else
            {
                //enemyMaster.CallEventEnemyLostTarget();
            }            
        }
    }

    bool CanPotentialTargetBeSeen(Transform potentialTarget)
    {
        Debug.DrawLine(detector.position, potentialTarget.position,Color.red, 1.0f);
        //Debug.DrawRay(detector.position, potentialTarget.position, Color.red, 1.0f);
        if (Physics.Linecast(detector.position, potentialTarget.position, out hit, sightLayer))
        //if (Physics.Raycast(detector.position, potentialTarget.position, out hit, 200))
        {
            //jos linecast osuu pelaajaan niin asetetaan pelaaja navmesh agentin kohteeksi
            if (hit.transform == potentialTarget)
            {
                enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
                return true;
            }

            else
            {
                //enemyMaster.CallEventEnemyLostTarget();
                return false;
            }            
        }

        else
        {
           // enemyMaster.CallEventEnemyLostTarget();
            return false;
        }
    }

    void TryToMoveTowardsTarget()
    {
        //jos kohde on asetettu niin yritetään siirtyä ampumisetäisyydelle
        if (enemyMaster.target != null && nm != null && !enemyMaster.isNavPaused)
        {
            nm.SetDestination(enemyMaster.target.position);

            if (nm.remainingDistance > nm.stoppingDistance)
            {
                enemyMaster.CallEventEnemyMoving();
                enemyMaster.isMoving = true;
            }
        }
    }

    void CheckIfDestinationReached()
    {
        if (enemyMaster.isMoving)
        {
            if (nm.remainingDistance < nm.stoppingDistance)
            {
                enemyMaster.isMoving = false;
                enemyMaster.CallEventEnemyReachedNavTarget();
            }
        }
    }

    void SetShotTarget(Transform targetTransform)
    {
        shotTarget = targetTransform;
    }

    void TryToShoot()
    {
        //kun ollaan päästy ampumaetäisyydelle niin ammutaan kohdetta
        if (shotTarget != null)
        {
            PlayerController player = shotTarget.GetComponent<PlayerController>();

            if (Time.time > nextShot && !enemyMaster.isMoving && enemyMaster.isAlive && player.alive)
            {
                nextShot = Time.time + shootRate;
                if (Vector3.Distance(enemyTransform.position, shotTarget.position) <= shotRange)                    
                {
                    if (Physics.Linecast(detector.position, shotTarget.position, out hit, sightLayer))                       
                    {
                        if (hit.transform == shotTarget)
                        {
                            Vector3 lookAtVector = new Vector3(shotTarget.position.x, enemyTransform.position.y, shotTarget.position.z);
                            enemyTransform.LookAt(lookAtVector);
                            enemyMaster.CallEventEnemyShootTarget();

                            Vector3 offset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.01f, 0.01f), Random.Range(-0.05f, 0.05f));
                            //Vector3 aim = new Vector3();
                            GameObject go = (GameObject)Instantiate(projectilePrefab, enemyGunBarrel.position, Quaternion.LookRotation(detector.forward + offset));
                            audio.pitch = Random.Range(0.9f, 1.1f);
                            audio.PlayOneShot(shot, 2.0f);
                        }
                    }                    
                }                   
            }
        }
    }
 
    void DisableThis()
    {
        enabled = false;
    }
}
