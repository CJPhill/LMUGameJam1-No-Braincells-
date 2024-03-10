using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint; // The point from where the projectile will be fired
    public GameObject bulletPrefab; // The projectile prefab
    public float bulletForce = 10f; // The speed of the projectile

    private Vector2 mousePos;

    private void Update()
    {
        LookAtMouse();
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Shoot();
        }
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector2)(mousePos - new Vector2(transform.position.x, transform.position.y));
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    
}
