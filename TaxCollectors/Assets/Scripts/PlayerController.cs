using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player movement
    private Animator animator;
    public GunController gunController;
    private GameManager gameManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();

    }

    void Update()
    {
        
        // Get input from the horizontal and vertical axes (default keys are arrows or WASD)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal == 0 && vertical == 0)
        {
            //isMoving animator true
            animator.SetBool("IsMoving", false);
        }
        animator.SetBool("IsMoving", true);

        // Create a movement vector based on the input
        Vector2 movement = new Vector2(horizontal, vertical);

        // Normalize the movement vector to ensure consistent speed in all directions
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player by adding the movement vector to the current position
        transform.Translate(movement, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            //player dies
            Destroy(gameObject);
            gameManager.playerDead = true;

        }
        else if (collision.gameObject.CompareTag("Ammo"))
        {
            gunController.AddAmmo();
            Destroy(collision.gameObject);
        }
    }
}
