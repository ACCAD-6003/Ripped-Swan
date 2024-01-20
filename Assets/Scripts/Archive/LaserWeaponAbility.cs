using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserWeaponAbility : MonoBehaviour
{
    [Range(1f, 100f)]
    public float maxDistance = 50f;

    //[Range(0f, 1f)] 
    private float duration = 0.05f;


    public KeyCode fireButton = KeyCode.Mouse0;

    [Tooltip("The tag of objects that can be damaged by the laser.")]
    public string tagMask = "laser-tag";


    [Tooltip("Name of the Character Controller")]
    public string nameOfCharacterController = "CharacterCapsulePlain";

    private Camera cam;
    private LineRenderer line;

    private Vector3 lookAt;

    private Transform playerTransform;


    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        playerTransform = GameObject.Find(nameOfCharacterController).GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool shouldShoot = Input.GetKeyDown(fireButton);
        if (shouldShoot)
        {
            Vector3 direction = GetShotDirection();
            Vector3 endPoint = playerTransform.position + direction * maxDistance;

            // Does the laser intersect any objects?
            RaycastHit hit;
            if (Physics.Raycast(playerTransform.position, direction, out hit, maxDistance))
            {
                // Don't let the laser effect go through objects
                endPoint = hit.point;


                // Apply damage to the object, if desired
                if (IsObjectShootable(hit.transform.gameObject))
                {
                    GameObject objectGettingShot = hit.transform.gameObject;
                    Response(objectGettingShot);
                }
            }

            // A primitive laser effect using the LineRenderer
            line.SetPosition(0, playerTransform.position);
            line.SetPosition(1, endPoint);
            StartCoroutine(TurnOffLaser());
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="o"></param>
    private void Response(GameObject o)
    {
        Destroy(o);
    }


    /// <summary>
    ///     Waits {laserDuration} before "turning off" the laser.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSecondsRealtime(duration);

        line.SetPosition(0, playerTransform.position);
        line.SetPosition(1, playerTransform.position);
    }


    /// <summary>
    ///     Is the object something we want to shoot?
    ///     You might modify this method to check the GameObject
    ///     for a certain tag, name or layer. For now I just check
    ///     if the GameObject has a rigidbody attached, as I
    ///     want to be able to shoot objects that have physics.
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    private bool IsObjectShootable(GameObject o) => o.CompareTag(tagMask);


    /// <summary>
    ///     What direction should the weapon be fired?
    ///     Here we always shoot in the XZ plane.
    /// </summary>
    /// <returns>Direction the player should shoot.</returns>
    private Vector3 GetShotDirection()
    {
        /*Vector3 p = Input.mousePosition;
        p.z = 20;
        Vector3 point = cam.ScreenToWorldPoint(p);
        Ray ray = cam.ScreenPointToRay(p);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lookAt = hit.point;
            lookAt.y = 0;
        }
        else
        {
            lookAt = point;
            lookAt.y = 0;
        }

        Vector3 movementDirection = lookAt - transform.position;
        movementDirection.y = 0;
        movementDirection.Normalize();*/

        Vector3 movementDirection = playerTransform.forward;

        return movementDirection;
    }
}