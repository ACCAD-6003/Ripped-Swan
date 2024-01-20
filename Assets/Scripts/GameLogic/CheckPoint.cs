using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkpoint;

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CheckPointManager.lastCheckPointPos = checkpoint.transform.position;
        }
    }

}
