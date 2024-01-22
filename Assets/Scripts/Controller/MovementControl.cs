using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{

    [SerializeField] private GameObject playerToMove;
    [SerializeField] private float speed = 5f;
    private InputAction moveAction;
    private Vector2 pMovement;

    // sets up and enables the movement input action
    public void Initialize(InputAction moveAction)
    {
        this.moveAction = moveAction;
        this.moveAction.Enable();
    }


    //reads value from movement actions and translattes them to vector3 for the player a can move in real time.
    void FixedUpdate()
    {
        pMovement = this.moveAction.ReadValue<Vector2>();
        Vector3 pVelocity = new Vector3(pMovement.x * speed, playerToMove.GetComponent<Rigidbody>().velocity.y, pMovement.y * speed);
        playerToMove.GetComponent<Rigidbody>().velocity = pVelocity;

    }
}
