using UnityEngine;
using System.Collections;

public class PlayerEnergy : MonoBehaviour
{

    public int energyLvl;
    public int maxEnergy;
    public int shotCost;
    public int shieldCost;

    public EnergyFillUp energyFillUp;

    public int rechargeRate = 1; // units per second

    private Coroutine rechargeCoroutine;

    public void StartEnergyRecharge()
    {
        
        if (rechargeCoroutine != null)
        {
            StopCoroutine(rechargeCoroutine);
        }

        rechargeCoroutine = StartCoroutine(RechargeEnergyOverTime());
    }

    private IEnumerator RechargeEnergyOverTime()
    {
        Debug.Log("Started recharging");

        while (energyLvl < maxEnergy && energyFillUp != null && energyFillUp.canCharge)
        {
            energyLvl += rechargeRate;
            
            yield return null;
        }

        Debug.Log("Stopped recharging (finished or exited)");
        rechargeCoroutine = null;

    }

    void Update()
    {
        // Optional: show real-time value
       // Debug.Log("Energy: " + energyLvl);
    }
}
