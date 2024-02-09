using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find any GameObject with the Swan script
            Swan swan = FindObjectOfType<Swan>();

            // Check if the Swan script is found
            if (swan != null)
            {
                // Add 5 to the enemiesKilled variable in Swan script
                Swan.enemiesKilled += 5;

                // Optionally, you can destroy the object or perform other actions
                Destroy(gameObject);  // Destroy the object that detected the player collision
            }
            else
            {
                Debug.LogError("Swan script not found in the scene.");
            }
        }
    }
}
