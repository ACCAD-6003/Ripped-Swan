using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanDestroy : MonoBehaviour
{
    public Collider playerCollider; // Reference to the player's collider
    public AudioSource destroySoundSource; // Reference to the AudioSource component

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other == playerCollider)
        {
            // Play the destroy sound
            destroySoundSource.Play();

            // Destroy only the GameObject with the collider (the child)
            Destroy(transform.gameObject);
        }
    }
}
