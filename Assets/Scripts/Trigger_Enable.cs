using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Enable : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
