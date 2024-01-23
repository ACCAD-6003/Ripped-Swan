using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{
    //Array of Gameobjects named "waypoint"
    GameObject[] waypoint;

    //Int variable named "rand"
    int rand;

    //GameObject variable named "waypointSelected"
    GameObject waypointSelected;

    //NavMeshAgent variable named "agent"
    UnityEngine.AI.NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Seed();
    }

    void Seed ()
    {
        waypoint = GameObject.FindGameObjectsWithTag("waypoint"); //filling array with all objects tagged "waypoint"
        rand = Random.Range(0, waypoint.Length); //picking random number from that array
        waypointSelected = waypoint[rand]; //setting waypointSelected to that random number 
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, waypointSelected.transform.position) >= 2)
        {
            // pursue 'waypointSelected'
            agent.SetDestination(waypointSelected.transform.position);
        }

        else
        {
            // if the distance is too small, find a new 'waypointSelected'
            Seed();
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
