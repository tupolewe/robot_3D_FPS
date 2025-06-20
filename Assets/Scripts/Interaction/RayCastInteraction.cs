using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float rayDistance = 3f;
    public LayerMask hitLayers;

    [Header("Interaction")]
    public bool canInteract;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (cam == null)
            Debug.LogError("RayCastInteraction: No camera found!");
    }

    void Update()
    {
        RayInteraction();
    }

    public void RayInteraction()
    {
        if (cam == null) return;

        // Ray from center of the screen
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, hitLayers))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            //Debug.Log("Hit: " + hit.collider.name);


            Interactable interactable = hit.collider.GetComponent<Interactable>();
            canInteract = interactable != null;

            if (canInteract && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
        else
        {
            canInteract = false;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
        }
    }
}
