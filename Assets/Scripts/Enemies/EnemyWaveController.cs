using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class EnemyWaveController : MonoBehaviour
{
    private bool notOver;
    public delegate void SpecialZoom();
    public static SpecialZoom specialZoom;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private int numberOfWaves = 3;
    [SerializeField] private float zoomTime = 2f;
    [SerializeField] private Transform fixedCameraPosition;
    [SerializeField] private float delayBeforeSpawn = 0.1f;
    [SerializeField] private float cameraAttachSpeed = 2f;
    [SerializeField] private GameObject arenaCollision;
    [SerializeField] private GameObject endLevelTrigger;

    [SerializeField] private GameObject arrowUI;
    [SerializeField] private float arrowDisplayTime = 3f;

    [SerializeField] private AudioSource spawnSound; // Reference to the AudioSource component
    [SerializeField] private Animator doorAnimator; // Reference to the Animator component
    [SerializeField] private bool isLastWave; // Checkbox to indicate if this is the last wave

    private Camera mainCamera;
    private FollowCamera followCameraScript;
    private bool isCameraDetached = false;
    private float originalCameraZPosition;
    private Quaternion originalCameraRotation;


    private void Start()
    {

       // specialZoom += BigZoom;
        if (Camera.main != null)
       {
           mainCamera = Camera.main;
        }
        followCameraScript = mainCamera.GetComponent<FollowCamera>();
        originalCameraZPosition = mainCamera.transform.position.z;
        originalCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(SpawnWaves());
    }

    // private void  OnDestroy()
   // {
    //    specialZoom -= BigZoom;
   // }

    IEnumerator SpawnWaves()
    {
        for (int wave = 1; wave <= numberOfWaves; wave++)
        {
           // Debug.Log($"Wave {wave} Incoming!");

            if (!isCameraDetached && wave == 1)
            {
               Debug.Log("Detaching Camera");
                yield return StartCoroutine(DetachCamera());
                
                //Enable the arena collision
                EnableCollision();
            }

            yield return new WaitForSeconds(delayBeforeSpawn);

            SpawnEnemies(wave);

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("enemy").Length == 0);

            if (wave == numberOfWaves)
            {
               Debug.Log("Attaching Camera");
                yield return StartCoroutine(AttachCamera());
                DisplayArrowUI();
                DisableCollision();

                // Better than having an unnecessary bool
                if (doorAnimator != null)
                {
                    // Trigger the door animation or any other actions for the last wave
                    PlayDoorAnimation();
                }



            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemies(int wave)
    {
        int numberOfEnemies = wave * 5;

        // Play the spawn sound
        spawnSound.Play();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints[i].position;
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
    }

    void DisplayArrowUI()
    {
        arrowUI.SetActive(true);
        StartCoroutine(HideArrowUI());
    }

    IEnumerator HideArrowUI()
    {
        yield return new WaitForSeconds(arrowDisplayTime);
        arrowUI.SetActive(false);
    }
    void EnableCollision()
    {
        arenaCollision.SetActive(true);
    }

    void DisableCollision()
    {
        arenaCollision.SetActive(false);
    }

    IEnumerator DetachCamera()
    {
        notOver = true;
        followCameraScript.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < cameraAttachSpeed)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, fixedCameraPosition.position, elapsedTime / cameraAttachSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, fixedCameraPosition.rotation, elapsedTime / cameraAttachSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.enabled = true;
        isCameraDetached = true;
    }

    IEnumerator AttachCamera()
    {
        notOver = false;
        followCameraScript.enabled = true;

        // Ensure camera position and rotation are reset
        mainCamera.transform.position = fixedCameraPosition.position;
        mainCamera.transform.rotation = fixedCameraPosition.rotation;

        // Delay for a short time to allow the camera to settle
        yield return new WaitForSeconds(0.1f);

        mainCamera.enabled = true;
        isCameraDetached = false;
    }


    //public void BigZoom()
  //  {
   //     Debug.Log("Big Zoom");
   //     StartCoroutine(Zoomer());

   // }

    private IEnumerator Zoomer()
    {
        // StartCoroutine(AttachCamera());
        followCameraScript.enabled = true;
        followCameraScript.offset = followCameraScript.zoomedOffset;
        yield return new WaitForSeconds(zoomTime);
        followCameraScript.offset = followCameraScript.OriginalOffset;
        if (notOver)
        {
            followCameraScript.enabled = false;
            float elapsedTime = 0f;
            while (elapsedTime < cameraAttachSpeed)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, fixedCameraPosition.position, elapsedTime / (cameraAttachSpeed));
                elapsedTime += Time.deltaTime;
            }
        }
            //mainCamera.transform.position = fixedCameraPosition.position;
            // StartCoroutine(DetachCamera());
        }
    private void PlayDoorAnimation()
    {
        // Check if this is the last wave and the doorAnimator is set
        //if (isLastWave)
        {
            // Set the DoorOpen parameter to trigger the animation
            doorAnimator.SetBool("DoorOpen", true);
            endLevelTrigger.SetActive(true);

        }
    }
}
