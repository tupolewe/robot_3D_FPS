using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Image shieldBar;
    public ShieldScript shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ColorStatus();
    }

    void ColorStatus()
    {
       if (shield.isActive == true) 
        {
            shieldBar.color = Color.blue;
            
        }
       else if (shield.isOnCooldown)
        {
            shieldBar.color = Color.red;
            
        }
        else if (shield.isActive == false && shield.isOnCooldown == false)
        {
            shieldBar.color = Color.green;
            
        }
    }
}
