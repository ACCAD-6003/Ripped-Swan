using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tr_EnableForSeconds : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] private float seconds = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(DisableAfterSeconds());
        }
    }

    IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(seconds);
        
        target.SetActive(false);
    }
}
