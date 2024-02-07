using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform originalPosition; // Original position of the camera
    public GameObject enemyGroup; // Reference to the enemy group GameObject
    public float smoothSpeed = 5f; // Smoothing factor for camera movement
    public Camera mainCamera;
    private bool cameraLocked = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cameraLocked)
        {
            MoveCameraToTarget();
        }
    }

    void Update()
    {
        if (cameraLocked)
        {
            // Check if all enemies are defeated
            if (AreAllEnemiesDefeated())
            {
                UnlockCamera();
            }
        }
    }

    void MoveCameraToTarget()
    {
        // Disable the FollowCamera script during camera lock
        FollowCamera followCameraScript = mainCamera.GetComponent<FollowCamera>();
        if (followCameraScript != null)
        {
            followCameraScript.enabled = false;
        }

        // Move the camera to the target position with smooth interpolation
        Vector3 desiredPosition = originalPosition.position;
        Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        mainCamera.transform.position = smoothedPosition;

        // Lock the camera
        cameraLocked = true;

        // Disable the trigger zone
        Debug.Log("Disabling trigger zone...");
        gameObject.SetActive(false);
    }

    void UnlockCamera()
    {
        // Enable the FollowCamera script when unlocking the camera
        FollowCamera followCameraScript = mainCamera.GetComponent<FollowCamera>();
        if (followCameraScript != null)
        {
            followCameraScript.enabled = true;
        }

        // Reset the camera to its original position with smooth interpolation
        Vector3 desiredPosition = originalPosition.position;
        Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        mainCamera.transform.position = smoothedPosition;

        // Unlock the camera
        cameraLocked = false;

        // Enable the trigger zone
        Debug.Log("Enabling trigger zone...");
        gameObject.SetActive(true);
    }


    bool AreAllEnemiesDefeated()
    {
        // Check if all enemies are destroyed in the group
        foreach (Transform enemy in transform)
        {
            // Assuming "Enemy" or "enemy" tags for individual enemies
            if (enemy.CompareTag("Enemy") || enemy.CompareTag("enemy"))
            {
                // If any enemy is found that is not destroyed, return false
                if (enemy.gameObject != null)
                {
                    Debug.Log("Enemy not destroyed: " + enemy.name);
                    return false;
                }
                else
                {
                    Debug.Log("Enemy destroyed: " + enemy.name);
                }
            }
        }

        // If all enemies are destroyed, return true
        Debug.Log("All enemies are destroyed!");
        return true;
    }


}
