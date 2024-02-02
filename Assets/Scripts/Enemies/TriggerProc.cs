using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerProc : MonoBehaviour
{

    [SerializeField] TriggerEnemies te;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int hold = transform.GetSiblingIndex();
            te.callLock(hold );
        }
    }
}
