using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SwanDeathState : ISwanState
{
    private SwanPunch swan;

    public SwanDeathState(SwanPunch swan)
    {
        this.swan = swan;
    }


    public void Update()
    {
        // TODO: If swan is dead, play death animation and show game over screen
        if (swan.state is SwanDeathState)
        {
            // TODO: Play death animation
            // TODO: Show game over screen

            //Scene thisScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(thisScene.name);
        }
    }


    // No need to implement, swan is dead
    public void Attack() {} 
    public void Die() {}

}