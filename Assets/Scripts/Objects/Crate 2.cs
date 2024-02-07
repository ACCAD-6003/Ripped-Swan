using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Crate : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<SwanController>().held)
            {
                Debug.Log("holding");
                rb.AddForce(MovementControl.direction * 10 * -1, ForceMode.Impulse); // pull towards player
            } else
            {
                rb.AddForce(MovementControl.direction * 10, ForceMode.Impulse); // push away from player
            }
        }
    }
}
