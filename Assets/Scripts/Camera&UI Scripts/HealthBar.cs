using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public List<GameObject> healthBars;

    private int maxHealth = 100;
    private int totalBars = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        HealthStatusCheck();
    }

    public void HealthStatusCheck()
    {
        if (playerHealth == null || healthBars == null || healthBars.Count == 0)
            return;

        
        int activeBars = Mathf.CeilToInt((playerHealth.health / (float)maxHealth) * totalBars);

        for (int i = 0; i < healthBars.Count; i++)
        {
            healthBars[i].SetActive(i < activeBars);
        }
    }
}
