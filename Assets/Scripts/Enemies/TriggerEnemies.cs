using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemies : MonoBehaviour
{
    [SerializeField] Transform enemyParent; //Parent of the enemies this will trigger
    [SerializeField] Transform CameraPositions;  //Positions for the camera to go  to
    [SerializeField] FollowCamera playerCam; //will set camera to an area
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //When all enemies are gone, free camera
    }

    public void callLock(int index)
    {
        playerCam.lockCamera(CameraPositions.GetChild(index));
    }
}
