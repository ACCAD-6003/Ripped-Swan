using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tr_DisableFromEnemy : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Debug.Log("Disable " + target);
            target.SetActive(false);
        }
    }
}
