using UnityEngine;
using UnityEngine.SceneManagement;

public class tr_lvl2 : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Load Next scene");
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }
}
