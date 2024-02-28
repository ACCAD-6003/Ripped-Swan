using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwanDestroy2 : MonoBehaviour
{
    public Collider playerCollider;
    public AudioSource destroySoundSource;
    public Image screenFadeImage;
    public TriggerCredits triggerCredits;

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

        // Enable the screen fade image
        screenFadeImage.enabled = true;

        // Wait for 10 seconds before triggering credits
        StartCoroutine(WaitAndTriggerCredits());
    }

    IEnumerator WaitAndTriggerCredits()
    {
        yield return new WaitForSeconds(10f);

        // Trigger the credits by enabling the associated GameObject
        triggerCredits.gameObject.SetActive(true);
    }
}

