using Assets.Scripts.Interfaces;
using UnityEngine;

internal class SwanDeathState : MonoBehaviour, ISwanState
{
    private Swan swan;

    public SwanDeathState(Swan swan)
    {
        this.swan = swan;
    }


    public void Update()
    {
        // TODO: If swan is dead, play death animation and show game over screen
        if (swan.state is SwanDeathState)
        {

        }
    }


    // No need to implement, swan is dead
    public void Attack() {} 
    public void Die() {}

}