using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;

    private GameObject lockedTarget;
    private bool locked;

    [Tooltip("How far the camera stays from the player.")]
    public Vector3 offset = new Vector3(0, -2, -12);

    [Tooltip("How far the camera stays from the player.")]
    public Vector3 zoomedOffset = new Vector3(0, 0, 2);

    [Tooltip("Speed at which the camera approaches the player's new position.")] [Range(0, 1)]
    public float smoothTime = 0.3F;


    [Tooltip("Fix the cameras z-axis.")]
    public bool fixedZ = true;

    private float val = 1;

    private Vector3 position;

    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
        locked = false;
    }

    private void Update()
    {
        if (target && shouldUpdate() && !locked)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.transform.position + offset;

            // Smoothly move the camera towards that target position
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            if (fixedZ)
            {
                Vector3 newPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
                transform.position = newPosition;
            } else
            {
                transform.position = smoothedPosition;
            }
        }

        else if (locked)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = lockedTarget.transform.position + offset;

            // Smoothly move the camera towards that target position
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            if (fixedZ)
            {
                Vector3 newPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                transform.position = smoothedPosition;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (target && !shouldUpdate() && !locked)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.transform.position + offset;

            // Smoothly move the camera towards that target position
            // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.smoothDeltaTime*val);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.smoothDeltaTime);
        }

        else if (locked)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = lockedTarget.transform.position + offset;

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


    public void lockCamera(Transform t)
    {
        locked = true;
        lockedTarget = t.gameObject;
    }

    public void freeCamera()
    {
        locked = false;
    }
}