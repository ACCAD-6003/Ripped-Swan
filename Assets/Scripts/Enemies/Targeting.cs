using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;

public class Targeting : MonoBehaviour
{
    [SerializeField] private float firingAngle=45f;
    [SerializeField] private float gravity = 10f;
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
        Vector3 lobDirection = (target.position - firePoint.position).normalized ; // Adjust the upward force as needed

        // Set the bullet's direction and speed
        bullet.GetComponent<Rigidbody>().velocity = lobDirection.normalized *5; // You may need to adjust the speed

       
    }

   /* 
    *  Fialed experiement with bullets
    * 
    * IEnumerator SimulateProjectile(Transform projectile)
    {
       

        // Move projectile to the position of throwing object + add some offset if needed.
        projectile.position = transform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(projectile.position, target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        projectile.rotation = Quaternion.LookRotation(target.position - projectile.position);
      
        Vector3 lobDirection = (target.position - firePoint.position).normalized + Vector3.up * 0.1f; // Adjust the upward force as needed
        Vector3 finalDirection = new Vector3(lobDirection.normalized.x * Vx, lobDirection.normalized.y * Vy*10f, lobDirection.normalized.z * Vx);

        // Set the bullet's direction and speed
        projectile.GetComponent<Rigidbody>().velocity = finalDirection; //lobDirection.normalized * 20f; // You may need to adjust the speed
        yield return null;
    } */

}
