using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{
    [SerializeField] Swan s;
    public static Vector3 direction;
    private GameObject playerToMove;
    [SerializeField] private float speed = 5f;
    private InputAction moveAction;
    private Vector2 pMovement;

    // sets up and enables the movement input action
    public void Initialize(InputAction moveAction)
    {
        playerToMove = this.gameObject;
        this.moveAction = moveAction;
        this.moveAction.Enable();

        direction = Vector3.right;
        transform.rotation = Quaternion.LookRotation(direction);
    }


    //reads value from movement actions and translattes them to vector3 for the player a can move in real time.
    void FixedUpdate()
    {
        if (s.state is SwanMoveState)
        {
            pMovement = this.moveAction.ReadValue<Vector2>();
            Vector3 pVelocity = new Vector3(pMovement.x * speed, playerToMove.GetComponent<Rigidbody>().velocity.y, pMovement.y * speed);
            playerToMove.GetComponent<Rigidbody>().velocity = pVelocity;
            if (pVelocity != Vector3.zero)
            {
                if (pVelocity.x > 0)
                {
                    // transform.rotation = Quaternion.LookRotation(playerToMove.GetComponent<Rigidbody>().velocity.normalized);
                    direction = Vector3.right;
                    transform.rotation = Quaternion.LookRotation(direction);
                }
                else if (pVelocity.x < 0)
                {
                    direction = Vector3.left;
                    transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }
    }
}
