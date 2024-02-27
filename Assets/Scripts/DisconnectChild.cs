using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectChild : MonoBehaviour
{
    [SerializeField] private Transform parentObject; // Drag the parent object from the Unity Editor
    [SerializeField] private Transform childToDisconnect; // Drag the child object from the Unity Editor

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust the tag as needed
        {
            DisconnectChildren();
        }
    }

    private void DisconnectChildren()
    {
        if (parentObject != null && childToDisconnect != null)
        {
            // Disconnect the child from its parent
            childToDisconnect.parent = null;

            Debug.Log("Child disconnected from parent.");
        }
        else
        {
            Debug.LogError("Parent object or child to disconnect is not assigned!");
        }
    }
}
