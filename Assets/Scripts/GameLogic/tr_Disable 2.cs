using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tr_Disable : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Disable " + target);
            target.SetActive(false);
        }
    }
}
