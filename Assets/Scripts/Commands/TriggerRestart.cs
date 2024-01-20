using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerRestart : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        print("trigger");
        if (col.gameObject.tag == "Player")
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
    }
}