using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private bool ready;
    [SerializeField] private float prepTime;
    [SerializeField] private float explodeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        ready = false;
        StartCoroutine(Launch());
    }


    IEnumerator Launch()
    {
        yield return new WaitForSeconds(prepTime);
        ready = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(ready)
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        gameObject.GetComponent<Collider>().isTrigger = true;
        transform.localScale += new Vector3(3f, 3f, 3f);

        yield return new WaitForSeconds(explodeTime);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Swan>().hit();
            
        }
    }
}
