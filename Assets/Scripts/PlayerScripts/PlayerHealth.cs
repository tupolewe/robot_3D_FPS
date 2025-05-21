using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public ShieldScript shield;

    

    public void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        if (shield.isActive == false) 
        {
            health -= amount;
            Debug.Log("Player took damage: " + amount);

            if (health <= 0)
            {
                Die();
            }
        }
        else if (shield.isActive == true) 
        {
           shield.ShieldDestroy();
        }
       
    }

    void Die()
    {
        Debug.Log("died");
        
    }


}