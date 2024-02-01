using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemies : MonoBehaviour
{
    [SerializeField] Transform enemyParent; //Parent of the enemies this will trigger
    [SerializeField] Transform wallParent;  //Are of the walls that will be enabled
    [SerializeField] FollowCamera playerCam; //will set camera to an area
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                enemyParent.GetChild(i).GetComponent<EnemyBehavior>().BeginWalk();
            }
        }
    }
}
