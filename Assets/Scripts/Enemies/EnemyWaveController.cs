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
    public float delayBeforeSpawn = 0.1f; // Adjust this for the delay before spawning enemies
    public float cameraAttachSpeed = 2f; // Adjust this for the smoothness of camera movement

    public GameObject arrowUI; // Reference to the arrow UI element
    public float arrowDisplayTime = 3f; // Adjust this for how long the arrow should be displayed

    private FollowCamera followCameraScript;
    private bool isCameraDetached = false;
    private float originalCameraZPosition;
    private Quaternion originalCameraRotation;

    private void Start()
    {
        // Get the FollowCamera script attached to the main camera
        followCameraScript = mainCamera.GetComponent<FollowCamera>();

        // Store the original camera Z position
        originalCameraZPosition = mainCamera.transform.position.z;
        originalCameraRotation = mainCamera.transform.rotation;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 1; wave <= numberOfWaves; wave++)
        {
            Debug.Log($"Wave {wave} Incoming!");

            // Detach camera on the first wave
            if (!isCameraDetached && wave == 1)
            {
                Debug.Log("Detaching Camera");
                yield return StartCoroutine(DetachCamera());
            }

            // Delay before spawning enemies
            yield return new WaitForSeconds(delayBeforeSpawn);

            SpawnEnemies(wave);

            // Wait until all enemies are defeated
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("enemy").Length == 0);

            // Reattach camera on the last wave
            if (wave == numberOfWaves)
            {
                Debug.Log("Attaching Camera");
                yield return StartCoroutine(AttachCamera());
                DisplayArrowUI(); // Display arrow UI after all waves are completed
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemies(int wave)
    {
        // Spawning logic - adjust as needed
        int numberOfEnemies = wave * 5;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints[i].position;
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
    }

    IEnumerator DetachCamera()
    {
        // Disable the FollowCamera script and enable a fixed camera with smooth movement
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
        // Enable the FollowCamera script and disable the fixed camera with smooth movement
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

    void DisplayArrowUI()
    {
        arrowUI.SetActive(true); // Activate the arrow UI
        StartCoroutine(HideArrowUI());
    }

    IEnumerator HideArrowUI()
    {
        yield return new WaitForSeconds(arrowDisplayTime);
        arrowUI.SetActive(false); // Deactivate the arrow UI after a certain time
    }
}
