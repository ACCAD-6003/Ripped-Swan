using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolFollow : MonoBehaviour
{

    GameObject[] waypoint;
    int rand;
    GameObject waypointSelected;
    UnityEngine.AI.NavMeshAgent agent;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Seed();
    }

    void Seed()
    {
        waypoint = GameObject.FindGameObjectsWithTag("waypoint");
        rand = Random.Range(0, waypoint.Length);
        waypointSelected = waypoint[rand];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, target.transform.position) < 5)
        {
            agent.SetDestination(target.transform.position);
            Seed();
        }

        else
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
    }

    
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
    }
    



}
