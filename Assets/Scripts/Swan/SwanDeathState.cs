using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SwanDeathState : ISwanState
{
    private Swan swan;

    public SwanDeathState(Swan swan)
    {
        this.swan = swan;
    }


    public void Update()
    {
        // TODO: If swan is dead, play death animation and show game over screen
        // TODO: Play death animation
        // TODO: Show game over screen
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
}