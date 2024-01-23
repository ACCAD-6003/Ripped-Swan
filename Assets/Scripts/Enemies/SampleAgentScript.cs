using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SampleAgentScript : MonoBehaviour
{
	public Transform target;
	UnityEngine.AI.NavMeshAgent agent;

	void Start ()
	{
	// define our Agent
	agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update ()
	{
	// set the destination of our Agent to 'target'
	agent.SetDestination(target.position);
	//agent.transform.LookAt(target);
	//agent.transform.Rotate(0,180,0);
	//agent.transform.Translate(Vector3.forward*Time.deltaTime*5);
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
