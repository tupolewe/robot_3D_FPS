using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public StateMachine stateMachine;
    public NavMeshAgent agent;

    public AIPath path;
    public NavMeshAgent Agent { get => agent; }

    [SerializeField] private string currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
