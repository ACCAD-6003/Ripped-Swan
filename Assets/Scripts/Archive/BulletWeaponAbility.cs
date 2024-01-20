using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletWeaponAbility : MonoBehaviour
{
    [Range(0, 10), Tooltip("Force at which the bullet is fired.")]
    public float bulletForce = 5f;

    [Range(0, 2), Tooltip("How far in front of the player the bullet is spawned.")]
    public float bulletOffset = 1f;

    public KeyCode bulletKey = KeyCode.Mouse2;

    [Header("Bullet object must contain a RigidBody.")]
    public GameObject bullet;


    [Range(1, 10), Tooltip("How long until the bullet gets deleted?")]
    public float bulletLifetime = 1f;

    [Range(1, 100), Tooltip("How many bullet can exist at once?")]
    public int bulletLimit = 20;

    [Tooltip("Name of the Character Controller")]
    public string nameOfCharacterController = "CharacterCapsulePlain";

    private Camera cam;

    private Vector3 lookAt;

    private int bulletCount;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        bulletCount = 0;
        playerTransform = GameObject.Find(nameOfCharacterController).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var shouldShoot = Input.GetKeyDown(bulletKey);
        if (shouldShoot && (bulletCount < bulletLimit || bulletLimit == 100))
        {
            bulletCount++;
            Vector3 direction = GetShotDirection();
            // Todo: add transform offset
            Vector3 positionInFrontOfPlayer = playerTransform.position + (direction * bulletOffset);
            var bulletCopy = GameObject.Instantiate(bullet, positionInFrontOfPlayer, playerTransform.rotation);
            bulletCopy.GetComponent<Rigidbody>().AddForce(direction * (bulletForce*90f+10f));
            StartCoroutine(DestroyBullet(bulletCopy));
        }
    }

    /// <summary>
    /// What direction should the weapon be fired?
    ///
    /// Here we always shoot in the XZ plane.
    /// </summary>
    /// <returns>Direction the player should shoot.</returns>
    private Vector3 GetShotDirection()
    {
        Vector3 p = Input.mousePosition;
        p.z = 20;
        Vector3 point = cam.ScreenToWorldPoint(p);
        Ray ray = cam.ScreenPointToRay(p);

        if (Physics.Raycast(ray, out var hit))
        {
            lookAt = hit.point;
            lookAt.y = 0;
        }
        else
        {
            lookAt = point;
            lookAt.y = 0;
        }

        Vector3 movementDirection = (lookAt - playerTransform.position);
        movementDirection.y = 0;
        movementDirection.Normalize();

        return movementDirection;
    }
    
    

    
    
    /// <summary>
    /// Waits {bulletLifetime} before "turning off" the laser.
    /// </summary>
    /// <param name="o">gameObject</param>
    /// <returns></returns>
    private IEnumerator DestroyBullet(GameObject o)
    {
        if (bulletLifetime == 10f)
        {
            bulletLifetime = float.MaxValue;
        }
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(o);
    }

}