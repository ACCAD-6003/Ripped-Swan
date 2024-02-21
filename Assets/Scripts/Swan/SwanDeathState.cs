using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SwanDeathState : ISwanState
{
    private Swan swan;

    public SwanDeathState(Swan swan)
    {
        this.swan = swan;
        swan.swanDeath.Play();
        //Time.timeScale = 0.1f;
    }


    public void Update()
    {
        if (!swan.swanDeath.isPlaying)
        {
            //Time.timeScale = 1.0f;
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
    }
}