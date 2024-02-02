using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SampleAgentScript : MonoBehaviour
{
    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    public GameObject bulletPrefab; // The bullet prefab
    public Transform firePoint; // The position from which the bullets are fired
    public float fireRate = 2.0f; // How many bullets to shoot per second

    public float proximityDistance = 10f; // The proximity distance for AI shooting

    public float rotationSpeed = 5.0f; // Adjust the rotation speed for smoothness

    private float nextFireTime;

    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Define our Agent
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Check if the player is within the proximity distance
        if (distanceToPlayer <= proximityDistance)
        {
            // Set the animator bool "Fire" to true to start the "Fire" animation
            animator.SetBool("Fire", true);

            // Check if it's time to fire a bullet
            if (Time.time > nextFireTime)
            {
                // Calculate the direction to the player
                Vector3 directionToPlayer = target.position - firePoint.position;
                directionToPlayer.Normalize();

                // Calculate the rotation to smoothly face the player
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Create a bullet and set its direction
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = directionToPlayer * 11.5f; // You may need to adjust the speed

                // Set the next time the AI can fire
                nextFireTime = Time.time + 1.0f / fireRate;
            }
        }
        else
        {
            // Set the animator bool "Fire" to false to stop the "Fire" animation
            animator.SetBool("Fire", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
    }
}
