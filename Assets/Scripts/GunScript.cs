using UnityEngine;
using StarterAssets;
using System.Xml.Serialization;

public class GunScript : MonoBehaviour
{

    private StarterAssetsInputs _input;
    public GameObject bulletPrefab;
    public GameObject bulletPoint;
    [SerializeField] private float bulletSpeed;

    public bool randomizeRecoil;
    public Vector2 randomRecoilConstrainsts;
    public Vector2 recoilPattern;

    public float weaponSwayAmount;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.shoot)
        {
            Shoot();
            _input.shoot = false;
        }

        RecoilSway();

    }

    public void Shoot()
    {
        //Debug.Log("shoot");'
        Recoil();
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed);


        RaycastHit bulletHit;
         if (Physics.Raycast(bulletPoint.transform.position, bulletPoint.transform.right * (-1f), out bulletHit, 1000f))
        {
            Debug.DrawRay(bulletPoint.transform.position, bulletPoint.transform.right * (-1000f), Color.red, 5f);
            //Debug.Log("Hit: " + bulletHit.collider.name);
        }
        else
        {
            Debug.DrawRay(bulletPoint.transform.position, bulletPoint.transform.right * (-1f), Color.green, 0.2f);
        }

    }

    public void Recoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
    }

    public void RecoilSway()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;
    }
}
