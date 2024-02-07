using UnityEngine;
using UnityEngine.SceneManagement;

public class tr_end : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Load Next scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}