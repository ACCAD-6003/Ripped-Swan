using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;

    [Tooltip("How far the camera stays from the player.")]
    public Vector3 offset = new Vector3(0, -2, -12);

    [Tooltip("Speed at which the camera approaches the player's new position.")] [Range(0, 1)]
    public float smoothTime = 0.3F;

    private float val = 1;

    private Vector3 position;

    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        if (target && shouldUpdate())
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.transform.position + offset;

            // Smoothly move the camera towards that target position
            // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.smoothDeltaTime*val);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.smoothDeltaTime);
        }
    }
    
    private void FixedUpdate()
    {
        if (target && !shouldUpdate())
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.transform.position + offset;

            // Smoothly move the camera towards that target position
            // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.smoothDeltaTime*val);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.smoothDeltaTime);
        }
    }

    private bool shouldUpdate()
    {
        var rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (rb.interpolation == RigidbodyInterpolation.None)
            {
                return false;
            }
            return true;
        }

        return true;
    }
}