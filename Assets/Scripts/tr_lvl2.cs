using UnityEngine;
using UnityEngine.SceneManagement;

public class tr_lvl2 : MonoBehaviour
{
    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Load Next scene");
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
