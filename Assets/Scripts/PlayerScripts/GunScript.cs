using UnityEngine;
using StarterAssets;

public class GunScript : MonoBehaviour
{
    public StarterAssetsInputs _input;
    public Camera playerCamera;
    public PlayerEnergy energy;

    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    public Vector2 recoilPattern;

    public float weaponSwayAmount;
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10f;
    public float recoilForce;

    public float fireRate = 0.2f; 
    public float nextTimeToFire = 0f;

    public PlayerMovement playerMovement;

    public float recoilX;
    public float recoilY;

    public AudioClip shotSound;
    public AudioSource src;
    public ParticleSystem shotVFX;

    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
        playerCamera = Camera.main; // Automatically get the main camera
        if (playerCamera == null)
        {
            Debug.LogError("GunScript: No Camera assigned or found!");
        }
    }

    void Update()
    {
        DetermineAim();

        if (_input.shoot && Time.time >= nextTimeToFire)
        {
            Shoot();
            nextTimeToFire = Time.time + fireRate;
            _input.shoot = false;
        }

        RecoilSway();
    }

    public void Shoot()
    {
        if (energy.energyLvl > energy.shotCost) 
        {
            energy.energyLvl -= energy.shotCost;
            Recoil();
            playerMovement.ApplyRecoil(recoilX, recoilY);
            shotVFX.Play();

            // Raycast from the center of the player's camera
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Middle of the screen
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1f);
                Debug.Log("Hit: " + hit.collider.name);

                if (hit.collider.GetComponent<Enemy>() != null)
                {

                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    enemy.TakeDamege();
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green, 0.2f);
            }

            src.pitch = Random.Range(1.1f, 1.2f);
            src.PlayOneShot(shotSound);
        }
       
    }

    public void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
    }

    public void Recoil()
    {
        transform.localPosition -= Vector3.forward * recoilForce;
    }

    public void RecoilSway()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000f;
    }

   

}
