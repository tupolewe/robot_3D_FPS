using UnityEngine;
using UnityEngine.UI;

public class DynamicScope : MonoBehaviour
{
    public Transform firePoint; 
    public RectTransform scopeUI;
    public Camera mainCamera;
    public float range = 500f;
    public LayerMask bulletLayer;

    void Update()
    {
        AdjustScope();
    }

    void AdjustScope()
    {
        RaycastHit hit;
        Vector3 hitPoint;

       
        if (Physics.Raycast(firePoint.position, firePoint.transform.right * (-1f), out hit, range))
        {
            //Debug.DrawRay(firePoint.position, firePoint.transform.right * (-1), Color.red, 5f); 
            hitPoint = hit.point; 
        }
        else
        {
            //Debug.DrawRay(firePoint.position, firePoint.transform.right * (-1), Color.red, 5f);
            hitPoint = firePoint.position + firePoint.forward * range; 
        }

        
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(hitPoint);

       
        scopeUI.position = screenPoint;
    }
}

