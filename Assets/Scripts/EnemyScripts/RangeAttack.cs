using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public float forwardForce = 30f;
    public float upForce = 5f;

    public Enemy enemy;
    public Transform player;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void PerformRangeAttack()
    {
        if (projectile == null || projectilePos == null || player == null) return;

        
        projectilePos.LookAt(player);

        
        Vector3 direction = (player.position - projectilePos.position).normalized;

        Rigidbody rb = Instantiate(projectile, projectilePos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(direction * forwardForce + Vector3.up * upForce, ForceMode.Impulse);

        
        BaseState currentState = enemy.stateMachine.activeState;
        if (currentState != null)
        {
            currentState.PlayRangeAttack();
        }
    }
}