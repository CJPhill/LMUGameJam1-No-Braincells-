using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint; // The point from where the projectile will be fired
    public GameObject bulletPrefab; // The projectile prefab

    public float bulletForce = 10f; // The speed of the projectile
    public int ammo = 50;

    public GameObject AmmoAlert;
    public float timer;

    public TextMeshProUGUI AmmoAlertTxt;
    public TextMeshProUGUI BulletsAmnt;

    private Vector2 mousePos;
    private void Start()
    {
        AmmoAlert.SetActive(false);
        timer = 5f;
    }

    private void Update()
    {
        if (ammo <=0)
        {
            ammoAlert();
        }
        LookAtMouse();
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Shoot();
        }
        SetBulletAmnt();
    }
    public void AddAmmo()
    {
        ammo = ammo + 15;
    }
    private void SetBulletAmnt()
    {
        BulletsAmnt.text = ammo.ToString() + " Bullets";
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector2)(mousePos - new Vector2(transform.position.x, transform.position.y));
    }

    private void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    private void ammoAlert()
    {
        if (ammo <= 0)
        {
            AmmoAlert.SetActive(true);
            timer -= Time.deltaTime; // Countdown the timer
            AmmoAlertTxt.text = "Time until Emergency resupply: " + timer.ToString();
        }
        if (timer <= 0)
        {
            AmmoAlert.SetActive(false);
            timer = 5f;
            ammo += 10;
        }
        
        
    }
    
}
