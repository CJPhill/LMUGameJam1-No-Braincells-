using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform; 
    public float speed = 5f;
    public BoxCollider2D boxCollider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction vector from the enemy's position to the player's position
            Vector3 direction = playerTransform.position - transform.position;
            direction.z = 0; // For 2D games, we don't want to move in the Z axis

            // Normalize the direction to get a unit vector, then move the enemy in that direction
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }
}
