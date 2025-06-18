using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    public int enemyHealth;

    

    public StateMachine stateMachine;
    public NavMeshAgent agent;

    public AIPath path;
    public AIPath searchPath;
    public NavMeshAgent Agent { get => agent; }

    [SerializeField] private string currentState;
    public bool hasHeardPlayer = false;

    public GameObject player;
    public float sightDistance;
    public float fieldOfView;
    public Vector3 lastKnownPlayerPosition;
    public float attackDistance;
    private bool playedOnce = false;
    

    public AudioSource src;
    public AudioSource src2;
    public AudioClip idleClip;
    public AudioClip attackClip;
    public AudioClip dieClip;
    
    
    void Start()
    {
        PlaySound();
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

   
    void Update()
    {
       
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null) 
        {
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance) 
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView) 
                {
                    Ray ray = new Ray(transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, sightDistance)) 
                    {
                        Debug.DrawLine(ray.origin, ray.direction * sightDistance);
                        if (hitInfo.transform.gameObject == player) 
                        {   lastKnownPlayerPosition = player.transform.position;
                            PlayAttackSound();
                            
                            return true;
                        }
                    }
                    
                }
                
            }
        }
        return false;
    }

    public bool CanHearPlayer()
    {
        if (hasHeardPlayer)
        {
            hasHeardPlayer = false;
            lastKnownPlayerPosition = player.transform.position;
            return true;
        }
        return false;
    }

    //public void OnTriggerEnter(Collider collider)
    //{
    //    if(collider.CompareTag("Player"))
    //    {
    //        Debug.Log("heard");
    //        hasHeardPlayer = true;
    //        lastKnownPlayerPosition = player.transform.position;

    //    }
    //}

  public void TakeDamege()
    {
        stateMachine.activeState.DamageAnim();
        int damage = UnityEngine.Random.Range(5, 12);

        enemyHealth -= damage;
        hasHeardPlayer = true;
        lastKnownPlayerPosition = player.transform.position;

        Debug.Log(enemyHealth);

        if (enemyHealth <= 0)
        {
            agent.isStopped = true;
            stateMachine.activeState.PlayDeath();
            stateMachine.enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(DieAfterDelay(1.5f));
        }
    }

    public void PlaySound()
    {
            src.loop = true;
            src.volume = 0.2f;
            src.clip = idleClip;
            src.Play();
            Debug.Log("gra idle");
        
        
    }
    public void PlayAttackSound()
    {
       if (!playedOnce)
        {
            src2.volume = 0.4f;
            src2.PlayOneShot(attackClip);
            playedOnce = true;
            PlaySound();
        }


    }

    public void ResetDamageAnim(Animator anim, float delay)
    {
        StartCoroutine(ResetDamageTrigger(anim, delay));
    }

    private IEnumerator ResetDamageTrigger(Animator anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.ResetTrigger("takesDamage?");
    }

    private IEnumerator DieAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        if (dieClip != null)
        {
            src2.PlayOneShot(dieClip);
        }

        Destroy(gameObject);
    }

}
