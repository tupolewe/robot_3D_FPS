using UnityEngine;

public class ShieldScript : MonoBehaviour
{

    public bool isActive;
    public void ShieldDestroy()
    {
        this.gameObject.SetActive(false);
    }
}
