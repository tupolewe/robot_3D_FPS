using UnityEngine;
using UnityEngine.ProBuilder;

public class RangeAttack : MonoBehaviour
{

    public GameObject projectile;
    public Transform projectilePos;
    public float forwardForce;
    public float upForce;
    public Transform player;
    public void PerformRangeAttack()
    {
        
        transform.LookAt(player);

       
        Rigidbody rb = Instantiate(projectile, projectilePos.position, projectilePos.rotation).GetComponent<Rigidbody>();

        
        rb.AddForce(projectilePos.forward * forwardForce, ForceMode.Impulse);
        rb.AddForce(projectilePos.up * upForce, ForceMode.Impulse);
    }
}
