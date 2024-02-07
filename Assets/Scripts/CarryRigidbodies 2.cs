using System.Collections.Generic;
using UnityEngine;

public class CarryRigidbodies : MonoBehaviour
{
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private List<Vector3> velocities = new List<Vector3>();

    private float threshold = 2f;

    private Vector3 lastPosition;
    private Transform _transform;

    private Vector3 lastVelocity = Vector3.zero;

    [Range(0f, 1f)]
    [Tooltip("How much of the platform's momentum you want the player to push off the wall with")]
    [SerializeField] private float momentumKept = 1;

    private MovingPlatform movingPlatform;

    private void Start()
    {
        _transform = transform;
        lastPosition = transform.position;
        movingPlatform = GetComponent<MovingPlatform>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _transform.position - lastPosition;
        lastVelocity = velocity;

        foreach (Rigidbody rb in rigidbodies)
        {
            // rb.transform.Translate(velocity, _transform);
            // rb.MovePosition(rb.);
            // rb.velocity = velocity;

            rb.AddForce(velocity, ForceMode.Acceleration);
        }

        lastPosition = _transform.position;
    }

    private void OnCollisionStay(Collision other)
    {
        var rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null) Add(rb);
    }

    private void OnCollisionExit(Collision other)
    {
        var rb = other.gameObject.GetComponent<Rigidbody>();
        if(movingPlatform.goalPosition.x != 0)
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(momentumKept, 1, 1));
        }
        if (movingPlatform.goalPosition.y != 0)
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(1, momentumKept, 1));
        }
        if (movingPlatform.goalPosition.z != 0)
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(1, 1, momentumKept));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null) Remove(rb);
    }

    public void Add(Rigidbody rb)
    {
        if (!rigidbodies.Contains(rb))
        {
            print($"adding {rb.gameObject.name}");
            rigidbodies.Add(rb);
            rb.interpolation = RigidbodyInterpolation.None;
        }
    }

    public void Remove(Rigidbody rb)
    {
        if (rigidbodies.Contains(rb))
        {
            print($"removing {rb.gameObject.name}");
            rigidbodies.Remove(rb);

            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    public void SignalDirectionChange()
    {
        int i = rigidbodies.RemoveAll(item =>
        {
            if (item.transform.position.y - transform.position.y > threshold) return true;

            return false;
        });
    }
}