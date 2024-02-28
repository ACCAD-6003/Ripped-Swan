using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwanDestroy2 : MonoBehaviour
{
    public Collider playerCollider;
    public AudioSource destroySoundSource;
    public Image screenFadeImage;
    

    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider && !isFading)
        {
            PlayDestroySequence();
        }
    }

    void PlayDestroySequence()
    {
        isFading = true;

        // Play the destroy sound
        destroySoundSource.Play();

    }
}

