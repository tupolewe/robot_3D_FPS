using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

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
    
    void Start()
    {
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

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            Debug.Log("heard");
            hasHeardPlayer = true;
            lastKnownPlayerPosition = player.transform.position;

        }
    }

  public void TakeDamege()
    {
        int damage = UnityEngine.Random.Range(5, 12);

        enemyHealth -= damage;
        hasHeardPlayer = true;
        lastKnownPlayerPosition = player.transform.position;

        Debug.Log(enemyHealth);

        if(enemyHealth <= 0)
        {
            Debug.Log("deathenemy");
        }
    }
}
