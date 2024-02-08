using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // Array of spawn points
    public float timeBetweenWaves = 10f;
    public int numberOfWaves = 3;
    public Camera mainCamera;
    public Transform fixedCameraPosition;
    public float delayBeforeSpawn = 0.1f;
    public float cameraAttachSpeed = 2f;

    public GameObject arrowUI;
    public float arrowDisplayTime = 3f;

    public AudioSource spawnSound; // Reference to the AudioSource component

    private FollowCamera followCameraScript;
    private bool isCameraDetached = false;
    private float originalCameraZPosition;
    private Quaternion originalCameraRotation;

    private void Start()
    {
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
            }

            yield return new WaitForSeconds(delayBeforeSpawn);

            SpawnEnemies(wave);

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("enemy").Length == 0);

            if (wave == numberOfWaves)
            {
                Debug.Log("Attaching Camera");
                yield return StartCoroutine(AttachCamera());
                DisplayArrowUI();
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
