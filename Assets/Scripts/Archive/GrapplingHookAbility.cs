using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrapplingHookAbility : MonoBehaviour
{
    [Header("For pulling the player towards an object.")]

    public KeyCode pullPlayerKey = KeyCode.Mouse1;

    [Range(1f, 50f), Tooltip("The distance of the Grapple.")]
    public float maxDistance = 10f;

    [Range(1, 10)] [Tooltip("The speed at which the grapple is thrown.")]
    public float hookLaunchSpeed = 5;

    [Range(0.01f, 0.1f)] [Tooltip("Speed at which the player is pulled.")]
    public float hookPullSpeed = .01f;

    [Range(0, 1)] [Tooltip("Slight pause before pulling the player.")]
    public float hookDelaySeconds = 0.5f;

    [Tooltip("Tag of objects you want to use as grapples.")]
    public string grappleTag = "grapple-pull-player";

    [Tooltip("Name of the Character Controller")]
    public string nameOfCharacterController = "CharacterCapsulePlain";

    private Vector3 actualTipLocation;

    private Camera cam;

    private CombinedCharacterController characterController;

    private float grapplingHookRunTime;


    private Vector3 hitLocation;

    private LineRenderer line;

    private Vector3 lookAt;

    private State state = State.Idle;

    private float time;

    private bool wallContact;

    private Transform playerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        line = GetComponent<LineRenderer>();
        characterController = GetComponentInParent<CombinedCharacterController>();
        playerTransform = GameObject.Find(nameOfCharacterController).GetComponent<Transform>();
        grapplingHookRunTime = hookLaunchSpeed;
    }


    // Update is called once per frame
    private void Update()
    {
        // print(state);
        switch (state)
        {
            case State.Idle:
                characterController.SetGrounded(false);
                if (Input.GetKeyDown(pullPlayerKey))
                {
                    // Start the grapple
                    Vector3? direction = getShotDirection();

                    if (direction.HasValue)
                    {
                        state = State.Shooting;
                        // The max position the grapple can travel
                        hitLocation = playerTransform.position + direction.Value * maxDistance;

                        // Calculate actual target point
                        Debug.DrawRay(playerTransform.position, direction.Value * 1000, Color.red, 1f, true);
                        RaycastHit hit;
                        wallContact = false;
                        if (Physics.Raycast(playerTransform.position, direction.Value, out hit, maxDistance))
                        {
                            if (hit.collider.gameObject.CompareTag(grappleTag))
                            {
                                // If the grapple actually hits a wall, update the end position
                                hitLocation = hit.point;
                                print("hit");
                                // and set a flag
                                wallContact = true;
                            }
                        }
                    }
                }

                time = 0;
                break;
            case State.Shooting:
                // update shooting position
                line.forceRenderingOff = false;
                time += Time.deltaTime * hookLaunchSpeed;
                actualTipLocation = Vector3.Lerp(playerTransform.position, hitLocation, time);
                line.SetPosition(0, playerTransform.position);
                line.SetPosition(1, actualTipLocation);
                if (time > 1)
                {
                    time = 0;
                    if (wallContact)
                        state = State.Holding;
                    else
                        state = State.Returning;
                }

                break;
            case State.Holding:
                time += Time.deltaTime / hookDelaySeconds;
                // update the position
                line.SetPosition(0, playerTransform.position);
                line.SetPosition(1, actualTipLocation);
                // Do a short delay before pulling
                if (time > 1)
                {
                    time = 0;
                    state = State.Pulling;
                }

                break;
            case State.Pulling:
                float distanceToGoal = Vector3.Distance(playerTransform.position, hitLocation);
                float pullSpeed = hookPullSpeed * distanceToGoal;
                time += Time.deltaTime * pullSpeed;
                line.SetPosition(0, playerTransform.position);
                line.SetPosition(1, actualTipLocation);
                playerTransform.position = Vector3.Lerp(playerTransform.position, hitLocation, time);
                characterController.SetGrounded(true);
                if (distanceToGoal < 1f)
                {
                    time = 0;
                    line.forceRenderingOff = true;
                    state = State.Idle;
                }

                break;
            case State.Returning:
                time += Time.deltaTime;
                line.SetPosition(0, playerTransform.position);
                Vector3 actualTipPosition = Vector3.Lerp(hitLocation, playerTransform.position, time);
                line.SetPosition(1, actualTipPosition);
                if (time > 1)
                {
                    time = 0;
                    state = State.Idle;
                }

                break;
        }
    }


    /// <summary>
    ///     What direction should the grapple be fired?
    ///     Here we allow the player to grapple a point above
    ///     or below their Y position in the map.
    /// </summary>
    /// <returns>Direction the player should shoot.</returns>
    private Vector3? getShotDirection()
    {
        Vector3 p = Input.mousePosition;
        p.z = 20;
        Ray ray = cam.ScreenPointToRay(p);
        Vector3 point = cam.ScreenToWorldPoint(p);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.black, 2f, false);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            lookAt = hit.point;
        else
        {
            lookAt = ray.direction * 100;
            lookAt.y += playerTransform.position.y;
            print("no hit");
            Vector3 dir = playerTransform.position + ray.direction;
            Debug.DrawRay(point, Vector3.up * 10, Color.magenta, 2f, false);

            lookAt = point;
        }
        /* 
Vector3 movementDirection = lookAt - playerTransform.position;
movementDirection.y = 0;
movementDirection.Normalize();*/

        Vector3 movementDirection = playerTransform.forward;

        return movementDirection;
    }


    private enum State
    {
        Idle,
        Shooting,
        Holding,
        Pulling,
        Returning
    }
}