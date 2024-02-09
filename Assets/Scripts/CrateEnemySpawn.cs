using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateEnemySpawn : MonoBehaviour
{
    public Collider playerCollide; // Reference to the player's collider
    public AudioSource destroySound; // Reference to the AudioSource component
    public Transform spawnPosition; // Public variable for the spawn position
    public GameObject enemyPrefab; // Public variable for the enemy prefab

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other == playerCollide)
        {
            // Play the destroy sound
            destroySound.Play();

            // Destroy only the GameObject with the collider (the child)
            Destroy(transform.gameObject);

            // Spawn a single enemy at the specified position
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
    }
}
