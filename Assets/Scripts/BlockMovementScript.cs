using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovementScript : MonoBehaviour
{
    [SerializeField] private float pushbackMagnitude = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player character
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 displacement = collisionNormal * pushbackMagnitude;
            Vector3 newPosition = collision.collider.transform.position + displacement;

            // Set the new position for the character
            collision.collider.transform.position = newPosition;
        }
    }
}









