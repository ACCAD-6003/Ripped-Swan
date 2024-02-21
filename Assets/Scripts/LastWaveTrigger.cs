using System.Collections;
using UnityEngine;

public class LastWaveTrigger : MonoBehaviour
{
    [SerializeField] private Animation doorAnimation; // Reference to the Animation component
    [SerializeField] private GameObject endGameTrigger;

    private void Start()
    {
        // Subscribe to the event when the last wave starts
        EnemyWaveController.specialZoom += OnLastWaveStart;
    }

    private void OnLastWaveStart()
    {
        StartCoroutine(PlayDoorAnimation());
    }

    private IEnumerator PlayDoorAnimation()
    {
        if (doorAnimation != null)
        {
            doorAnimation.Play("DoorOpen"); // Adjust the animation name accordingly
        }

        yield return new WaitForSeconds(2f); // Adjust the time as needed for your door animation

        // Trigger the event when the last wave finishes
        EnemyWaveController.specialZoom?.Invoke();

        // Enable EndGameTrigger
        if (endGameTrigger != null)
        {
            endGameTrigger.SetActive(true);
        }
    }

    // Unsubscribe from the event when the object is disabled or destroyed
    private void OnDisable()
    {
        EnemyWaveController.specialZoom -= OnLastWaveStart;
    }

    private void OnDestroy()
    {
        EnemyWaveController.specialZoom -= OnLastWaveStart;
    }
}
