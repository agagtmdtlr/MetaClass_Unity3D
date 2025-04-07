using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon.WeaponType weaponType;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime;

    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        // Implement shooting logic here
        Debug.Log($"Firing {weaponType} from {weaponPrefab.name}");
    }
        
}