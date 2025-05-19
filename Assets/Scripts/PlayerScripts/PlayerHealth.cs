using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public bool shieldActive;
    public ShieldScript shield;

    [SerializeField] private GameObject electricSparks;

    public void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        if (shieldActive == false) 
        {
            health -= amount;
            Debug.Log("Player took damage: " + amount);

            if (health <= 0)
            {
                Die();
            }
        }
        else if (shieldActive == true) 
        {
           shield.ShieldDestroy();
        }
       
    }

    void Die()
    {
        Debug.Log("died");
        
    }



    public void ShieldDie()
    {
        shieldActive = false;
        shield.ShieldDestroy();
        Debug.Log("shield desctricojdnads");

    }
}