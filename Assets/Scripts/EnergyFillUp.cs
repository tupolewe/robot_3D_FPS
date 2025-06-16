using UnityEngine;

public class EnergyFillUp : MonoBehaviour, Interactable
{
    public PlayerEnergy playerEnergy;
    public bool canCharge;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canCharge = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCharge = false;
        }
    }
    public void Interact()
    {
        playerEnergy.energyFillUp = GetComponent<EnergyFillUp>();
        if (playerEnergy != null)
        {
            Debug.Log("energy fill up1");
            playerEnergy.StartEnergyRecharge();
        }
    }
}
