using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class WaveTrigger : MonoBehaviour
{
    public EnemyWaveController waveController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            waveController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
