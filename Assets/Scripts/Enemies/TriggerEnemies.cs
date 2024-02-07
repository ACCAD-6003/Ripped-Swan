using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemies : MonoBehaviour
{
    private int enemyGroup;
    [SerializeField] Transform enemyParent; //Parent of the enemies this will trigger
    [SerializeField] Transform CameraPositions;  //Positions for the camera to go to
    [SerializeField] FollowCamera playerCam; //will set camera to an area

    // Start is called before the first frame update
    void Awake()
    {
        enemyGroup = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyGroup < enemyParent.childCount)
        {
            if (enemyParent.GetChild(enemyGroup).childCount == 0)
            {
                // Free camera and set the desired position
                playerCam.freeCamera();
                // SetCameraPositionOnUnlock(playerCam.transform, 20f, -8.5f);
                enemyGroup++;
            }
        }
        //When all enemies are gone, free camera
    }

    //private void SetCameraPositionOnUnlock(Transform cameraTransform, float y, float z)
    //{
    //  Vector3 newPosition = new Vector3(cameraTransform.position.x, y, z);
    //cameraTransform.position = newPosition;
    //}

    public void callLock(int index)
    {
        playerCam.lockCamera(CameraPositions.GetChild(index));
        for (int i = 0; i < enemyParent.GetChild(enemyGroup).childCount; i++)
        {
            StartCoroutine(enemyParent.GetChild(enemyGroup).GetChild(i).GetComponent<EnemyBehavior>().BeginWalk());
        }
    }
}