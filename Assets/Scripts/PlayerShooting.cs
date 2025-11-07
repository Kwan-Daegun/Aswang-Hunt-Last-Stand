using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    [Header("Ammo Settings")]
    public int maxAmmo = 10;         // Maximum ammo the player can hold
    private int currentAmmo;         // Current ammo count

    void Start()
    {
        currentAmmo = maxAmmo;       // Fill ammo at start
        UIManager.Instance.UpdateAmmo(currentAmmo, maxAmmo);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }

        // Optional: reload manually (press R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Press R to reload.");
            return;
        }

        currentAmmo--;   // Reduce ammo when shooting
        UIManager.Instance.UpdateAmmo(currentAmmo, maxAmmo);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = new Vector2(direction * bulletSpeed, 0f);
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x *= direction;
            bullet.transform.localScale = bulletScale;
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        UIManager.Instance.UpdateAmmo(currentAmmo, maxAmmo);
        Debug.Log("Reloaded!");
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }


}
