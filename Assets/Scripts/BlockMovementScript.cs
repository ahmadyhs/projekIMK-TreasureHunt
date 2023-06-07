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
            Debug.Log("hmm");
            Vector3 collisionNormal = collision.GetContact(1).normal;
            Vector3 displacement = collisionNormal * -pushbackMagnitude;
            Vector3 newPosition = collision.collider.transform.position + displacement;

            // Set the new position for the character
            if(collisionNormal != Vector3.down)
                collision.collider.transform.position = newPosition;
        }
    }
}









