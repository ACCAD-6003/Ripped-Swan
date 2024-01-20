using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrappleObjectsAbility : MonoBehaviour
{
    [Header("For pulling an object towards the player.")]

     public KeyCode pullObjectKey = KeyCode.Mouse1;

    [Range(1f, 50f), Tooltip("The distance of the Grapple.")]
    public float maxDistance = 10f;

    [Range(1, 10), Tooltip("The speed at which the grapple is thrown.")]
    public float hookLaunchSpeed = 5;

    [Range(0.01f, 0.1f), Tooltip("Speed at which the player is pulled.")]
    public float hookPullSpeed = .01f;

    [Range(0, 1), Tooltip("Slight pause before pulling the player.")]
    public float hookDelaySeconds = 0.5f;

    [Range(1, 10), Tooltip("How close the object is pulled to the player.")]
    public float distanceFromPlayer = 3f;

    [Tooltip("Tag of objects you want to use as grapples.")]
    public string grappleTag = "grapple-pull-object";

    [Tooltip("Name of the Character Controller")]
    public string nameOfCharacterController = "CharacterCapsulePlain";


    private enum State
    {
        Idle,
        Shooting,
        Holding,
        Pulling,
        Returning
    }


    private Vector3 hitLocation;
    private Vector3 actualTipLocation;

    private Camera cam;
    private LineRenderer line;

    private Vector3 lookAt;

    private GameObject pullObject;

    private State state = State.Idle;

    private CombinedCharacterController characterController;

    private Transform playerTransform;

    /// <summary>
    /// This method is called when the object reaches
    /// our player, and the grapple is released.
    ///
    /// Use this method to add items to an inventory or etc.
    /// </summary>
    /// <param name="o">the object that was being pulled</param>
    void objectReachedPlayer(GameObject o)
    {
        // Implement your functionality here
        print(o.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        line = GetComponent<LineRenderer>();
        characterController = GetComponentInParent<CombinedCharacterController>();
        playerTransform = GameObject.Find(nameOfCharacterController).GetComponent<Transform>();
    }

    private float time;

    private bool wallContact;

    // Update is called once per frame
    void Update()
    {
        // print(state);
        switch (state)
        {
            case State.Idle:
                if (Input.GetKeyDown(pullObjectKey))
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
                        wallContact = false;
                        if (Physics.Raycast(playerTransform.position, direction.Value, out var hit, maxDistance))
                        {
                            if (hit.collider.gameObject.CompareTag(grappleTag))
                            {
                                // If the grapple actually hits a wall, update the end position
                                hitLocation = hit.point;
                                // and set a flag
                                wallContact = true;
                                pullObject = hit.collider.gameObject;
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
                    {
                        state = State.Holding;
                    }
                    else
                    {
                        state = State.Returning;
                    }
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
                float distanceToGoal = Vector3.Distance(pullObject.transform.position, playerTransform.position);
                float pullSpeed = hookPullSpeed * distanceToGoal;
                time += Time.deltaTime * pullSpeed;
                line.SetPosition(0, playerTransform.position);
                line.SetPosition(1, pullObject.transform.position);
                pullObject.transform.position = Vector3.Lerp(pullObject.transform.position, playerTransform.position, time);
                //characterController.SetGrounded(true);
                if (distanceToGoal < distanceFromPlayer)
                {
                    time = 0;
                    line.forceRenderingOff = true;
                    state = State.Idle;
                    objectReachedPlayer(pullObject);
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
    /// What direction should the grapple be fired?
    ///
    /// Here we allow the player to grapple a point above
    /// or below their Y position in the map.
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
        {
            lookAt = hit.point;
        }
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
        Vector3 movementDirection = lookAt - transform.position;
        movementDirection.y = 0;
        movementDirection.Normalize();*/
        Vector3 movementDirection = playerTransform.forward;
        return movementDirection;
    }
}