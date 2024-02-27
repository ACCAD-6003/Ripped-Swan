using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachObjectOnAnimation : MonoBehaviour
{
    public string animationName = "LevelStart2"; // Replace with the actual animation name
    public GameObject objectToDetach; // Specify the object you want to detach

    private bool hasDetached = false;

    private void Update()
    {
        Animator animator = GetComponent<Animator>();

        // Check if the specified animation is currently playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // Ensure the object is detached only once
            if (!hasDetached)
            {
                DetachObject();
                hasDetached = true;
            }
        }
        else
        {
            // Reset the detachment status when the animation is not playing
            hasDetached = false;
        }
    }

    private void DetachObject()
    {
        // Check if the object to detach is not null and has a parent
        if (objectToDetach != null && objectToDetach.transform.parent != null)
        {
            // Detach the object from its parent
            objectToDetach.transform.parent = null;
        }
    }
}
