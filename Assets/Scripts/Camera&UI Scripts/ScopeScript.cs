using UnityEngine;
using UnityEngine.UI;

public class ScopeScript : MonoBehaviour
{
    public RectTransform scopeUI;
    public Camera mainCamera;
    public float range = 1000f;
    public LayerMask bulletLayer;

    void Update()
    {
        AdjustScope();
    }

    void AdjustScope()
    {
        if (mainCamera == null)
        {
            Debug.LogError("ScopeScript: No Camera assigned!");
            return;
        }

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 hitPoint;

        if (Physics.Raycast(ray, out hit, range, bulletLayer))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = ray.origin + ray.direction * range; 
        }

        Vector3 screenPoint = mainCamera.WorldToScreenPoint(hitPoint);

        if (screenPoint.z > 0) 
            scopeUI.position = screenPoint;
        }
    }

