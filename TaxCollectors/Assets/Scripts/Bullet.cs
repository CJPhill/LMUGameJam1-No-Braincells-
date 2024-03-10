using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private Camera mainCamera;
    private GameManager gameManager;
    public int chance = 10;
   
    private void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindAnyObjectByType<GameManager>();

        
    }

    private void Update()
    {
        if (!IsVisible())
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy")){
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(collision.gameObject);
            gameManager.AddScore(100);
            gameManager.SpawnAmmoBox(15, (Vector2)transform.position);



        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Ammo"))
        {
            Destroy(gameObject);

        }
        
    }

    bool IsVisible()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        return (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1);
    }
}
