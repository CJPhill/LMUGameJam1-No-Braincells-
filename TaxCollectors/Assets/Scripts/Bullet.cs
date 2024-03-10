using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
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
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        if (collision.gameObject.CompareTag("Enemy")){
            Destroy(collision.gameObject);

        }
        if (!collision.gameObject.CompareTag("Player"))
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
