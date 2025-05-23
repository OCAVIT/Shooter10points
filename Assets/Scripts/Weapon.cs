using UnityEngine;
using Mirror;

public class Weapon : NetworkBehaviour
{
    public int maxAmmo = 10;
    [SyncVar] public int currentAmmo;
    public float reloadTime = 2f;
    public float fireRate = 0.3f;
    public float damage = 20f;
    public Camera fpsCam;
    public LayerMask hitMask;

    bool isReloading = false;
    float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        if (currentAmmo <= 0) return;
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100f, hitMask))
        {
            var target = hit.transform.GetComponent<PlayerHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}