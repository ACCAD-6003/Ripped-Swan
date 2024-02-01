using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tr_Destroy : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void OnCollisionEnter()
    {
        Destroy(target);
    }
}
