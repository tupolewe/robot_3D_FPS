using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public StateMachine stateMachine;
    public NavMeshAgent agent;

    public AIPath path;
    public AIPath searchPath;
    public NavMeshAgent Agent { get => agent; }

    [SerializeField] private string currentState;

    public GameObject player;
    public float sightDistance;
    public float fieldOfView;
    public Vector3 lastKnownPlayerPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
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
}
