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
    private Animator animator;
    private AudioSource audioSource;
    private GameManager gameManager;
    public AudioSource emergencyReload;

    private void Start()
    {
        AmmoAlert.SetActive(false);
        timer = 5f;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (timer > 0 && ammo > 0)
        {
            AmmoAlert.SetActive(false);
        }
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
        if (gameManager.playerDead == true)
        {
            timer = 0;
            ammo = 20;
            AmmoAlert.SetActive(false);
        }
        
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
            audioSource.Play();
            ammo--;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            animator.SetTrigger("Shoot");
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
            emergencyReload.Play();
        }
        
        
    }
    
}
