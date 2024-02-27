using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCredits : MonoBehaviour
{
    public EndCredits endCredits;
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("TriggerCredits");
            endCredits.StartCredits();
        }
    }
}