
using UnityEngine;
using UnityEngine.UIElements;

public class RayCastInteraction : MonoBehaviour
{
    public float sphereRadius;
    public float rayDistance;
    public LayerMask hitLayers;


    public bool canInteract;
    

    // Update is called once per frame
    void Update()
    {
        RayInteraction();
    }

    public void RayInteraction()
    {


        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        

        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, rayDistance, hitLayers))
        {
            //Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red); 
            if(hit.collider != null) 
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null && Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
                
            }
         

        }
        else
        {
            
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green);
        }

    }


    
}
