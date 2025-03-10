using UnityEngine;
using StarterAssets;
using System.Xml.Serialization;

public class GunScript : MonoBehaviour
{

    public StarterAssetsInputs _input;
    public GameObject bulletPoint;

    public bool randomizeRecoil;
    public Vector2 randomRecoilConstrainsts;
    public Vector2 recoilPattern;

    public float weaponSwayAmount;

    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public float aimSmooting = 10;

    

    public float recoilForce;

    public PlayerMovement playerMovement;


    public float recoilX;
    public float recoilY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
        Debug.Log("input");
    }

    // Update is called once per frame
    void Update()
    {
        DetermineAim();

        if (_input.shoot)
        {
            Debug.Log("shoot");
            Shoot();
            _input.shoot = false;
        }

        RecoilSway();

    }

    public void Shoot()
    {
        Recoil();
        playerMovement.ApplyRecoil(recoilX, recoilY);
        Debug.Log("shoot2");
        RaycastHit bulletHit;
         if (Physics.Raycast(bulletPoint.transform.position, bulletPoint.transform.right * (-1f), out bulletHit, 1000f))
        {
            Debug.DrawRay(bulletPoint.transform.position, bulletPoint.transform.right * (-1000f), Color.red, 5f);
            Debug.Log("Hit: " + bulletHit.collider.name);
            if(bulletHit.collider.GetComponent<EnemyScript>() != null)
            {
                bulletHit.collider.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.DrawRay(bulletPoint.transform.position, bulletPoint.transform.right * (-1f), Color.green, 0.2f);
        }

    }


    public void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmooting);

        transform.localPosition = desiredPosition;

    }

    public void Recoil()
    {
        transform.localPosition -= Vector3.forward * recoilForce;
    }

    public void RecoilSway()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;
    }
}
