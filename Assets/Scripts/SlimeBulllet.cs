using UnityEngine;

public class SlimeBulllet : MonoBehaviour
{

    public int minDamage = 5;
    public int maxDamage = 12;

    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                int damageAmount = Random.Range(minDamage, maxDamage + 1); 
                playerHealth.TakeDamage(damageAmount);
            }
        }

        //if(other.CompareTag("RangeEnemy") == false)
        //{
        //   SlimeCollisionAnim();
        //    Destroy(gameObject);
        //}

        
    }

    public void SlimeCollisionAnim()
    {
        animator.SetBool("collision", true); 
    }
}
