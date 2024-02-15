using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SampleAgentScript : MonoBehaviour
{
    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    public GameObject bulletPrefab; // The bullet prefab
    public Transform firePoint; // The position from which the bullets are fired
    public float burstRate = 1.0f / 3.0f; // How many bursts to shoot per second
    public int burstCount = 3; // Number of bullets per burst

    public float proximityDistance = 10f; // The proximity distance for AI shooting

    public float rotationSpeed = 5.0f; // Adjust the rotation speed for smoothness

    private float nextBurstTime;

    //private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Define our Agent
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
       // animator = GetComponent<Animator>();

        // Set the initial burst time
        nextBurstTime = Time.time;
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // Check if the player is within the proximity distance
        if (distanceToPlayer <= proximityDistance)
        {
            // Set the animator bool "Fire" to true to start the "Fire" animation
            //animator.SetBool("Fire", true);

            // Check if it's time for a burst
            if (Time.time > nextBurstTime)
            {
                // Fire the burst
                FireBullet();

                // Set the next time for the next burst
                nextBurstTime = Time.time + burstRate;
            }
        }
        //else
        {
            // Set the animator bool "Fire" to false to stop the "Fire" animation
            //animator.SetBool("Fire", false);
        }
    }

    void FireBullet()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = target.position - firePoint.position;
        directionToPlayer.Normalize();

        // Use LookAt to smoothly face the player
        firePoint.LookAt(target);

        // Calculate the lob trajectory by adding an upward force
        Vector3 lobDirection = directionToPlayer + Vector3.up * 0.5f; // Adjust the upward force as needed

        // Introduce a small delay between each bullet instantiation
        float delayBetweenBullets = 0.2f; // Adjust as needed

        // Fire the burst with a slight delay between bullets
        for (int i = 0; i < burstCount; i++)
        {
            Invoke("SpawnBullet", i * delayBetweenBullets);
        }
    }

    void SpawnBullet()
    {
        // Create a bullet and set its direction
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's collider as a trigger
        bullet.GetComponent<Collider>().isTrigger = false ;

        // Calculate the lob trajectory by adding an upward force
        Vector3 lobDirection = (target.position - firePoint.position).normalized + Vector3.up * 0.1f; // Adjust the upward force as needed

        // Set the bullet's direction and speed
        bullet.GetComponent<Rigidbody>().velocity = lobDirection.normalized * 11f; // You may need to adjust the speed
    }

   
}
