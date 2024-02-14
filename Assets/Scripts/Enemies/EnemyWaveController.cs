using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private int numberOfWaves = 3;
    
    [SerializeField] private Transform fixedCameraPosition;
    [SerializeField] private float delayBeforeSpawn = 0.1f;
    [SerializeField] private float cameraAttachSpeed = 2f;
    [SerializeField] private GameObject arenaCollision;

    [SerializeField] private GameObject arrowUI;
    [SerializeField] private float arrowDisplayTime = 3f;

    [SerializeField] private AudioSource spawnSound; // Reference to the AudioSource component

    private Camera mainCamera;
    private FollowCamera followCameraScript;
    private bool isCameraDetached = false;
    private float originalCameraZPosition;
    private Quaternion originalCameraRotation;

    private void Start()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
        }
        followCameraScript = mainCamera.GetComponent<FollowCamera>();
        originalCameraZPosition = mainCamera.transform.position.z;
        originalCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 1; wave <= numberOfWaves; wave++)
        {
            Debug.Log($"Wave {wave} Incoming!");

            if (!isCameraDetached && wave == 1)
            {
                Debug.Log("Detaching Camera");
                yield return StartCoroutine(DetachCamera());
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
        followCameraScript.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < cameraAttachSpeed)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, fixedCameraPosition.position, elapsedTime / cameraAttachSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, fixedCameraPosition.rotation, elapsedTime / cameraAttachSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.GetComponent<Camera>().enabled = true;
        isCameraDetached = true;
    }

    IEnumerator AttachCamera()
    {
        followCameraScript.enabled = true;

        float elapsedTime = 0f;
        while (elapsedTime < cameraAttachSpeed)
        {
            Vector3 targetPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, originalCameraZPosition);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, elapsedTime / cameraAttachSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, originalCameraRotation, elapsedTime / cameraAttachSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.GetComponent<Camera>().enabled = true;
        isCameraDetached = false;
    }
}
