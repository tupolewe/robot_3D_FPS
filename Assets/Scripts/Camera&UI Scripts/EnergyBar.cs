using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public PlayerEnergy energy;
    public Image energyBar;

    
    public List<GameObject> energyBars;

    private int maxHealth = 500;
    private int totalBars = 10;

    void Update()
    {
        EnergyCheckStatus();
        ColorStatus();
    }

    public void EnergyCheckStatus()
    {
        if (energy == null || energyBars == null || energyBars.Count == 0)
            return;

        // Calculate number of bars to activate based on energy level
        int activeBars = energy.energyLvl <= 0 ? 0 : Mathf.CeilToInt((energy.energyLvl / (float)maxHealth) * totalBars);

        // Enable/disable energy bar objects
        for (int i = 0; i < energyBars.Count; i++)
        {
            energyBars[i].SetActive(i < activeBars);
        }
    }

    public void ColorStatus()
    {
        if(energy.energyLvl > 0) 
        {
            energyBar.color = Color.blue;
        }
        else if (energy.energyLvl <= 0)
        {
            energyBar.color = Color.red;
        }

    }
}

