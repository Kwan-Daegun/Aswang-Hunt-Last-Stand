using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;     
    public Transform firePoint;         
    public float bulletSpeed = 10f;     

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;        
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
}
