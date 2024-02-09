using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Collider playerCollider; // Reference to the player's collider
    public AudioSource destroySoundSource; // Reference to the AudioSource component
    public Transform spawnPosition; // Public variable for the spawn position
    public GameObject[] ItemPrefabs; // Array of item prefabs, including nothing

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other == playerCollider)
        {
            // Play the destroy sound
            destroySoundSource.Play();

            // Destroy only the GameObject with the collider (the child)
            Destroy(transform.gameObject);

            // Randomly choose and spawn an item at the specified position
            SpawnRandomItem();
        }
    }

    private void SpawnRandomItem()
    {
        // Check if there are any item prefabs in the array
        if (ItemPrefabs.Length > 0)
        {
            // Randomly choose an index from the array
            int randomIndex = Random.Range(0, ItemPrefabs.Length);

            // Spawn the selected item prefab at the specified position
            Instantiate(ItemPrefabs[randomIndex], spawnPosition.position, Quaternion.identity);
        }
        // If the array is empty, do nothing
    }
}

