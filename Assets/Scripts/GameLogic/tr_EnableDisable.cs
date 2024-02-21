using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tr_EnableDisable : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    [Tooltip("True enables target gameobject, false disables")]
    [SerializeField] private bool enableOrDisable;

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Disable " + target);
            target.SetActive(enableOrDisable);
            gameObject.SetActive(false);
        }
    }
}
