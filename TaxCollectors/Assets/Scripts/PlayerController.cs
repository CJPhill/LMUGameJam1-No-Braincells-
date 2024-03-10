using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player movement

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes (default keys are arrows or WASD)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

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
        }
    }
}
