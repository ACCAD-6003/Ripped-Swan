using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tr_Disable : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void OnCollisionEnter()
    {
        Debug.Log("Disable " + target);
        target.SetActive(false);
    }
}
