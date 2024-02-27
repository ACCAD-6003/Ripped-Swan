using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class lvl1_end_cutscene : MonoBehaviour
{
    [SerializeField] private GameObject _swanController;
    //[SerializeField] private SwanController _swanController;
    [SerializeField] private Animator _cutsceneAnimator;
    //[SerializeField] private Animator _trainAnimator;
    int keyframes = 360;
    int keyframesPerSecond = 60;
    
    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            /*
                disabling player control by disabling the swan controller
                start the animator for the swan where the swan walks into the train
                zoom the camera out
                start the animation for the train
                (optional) vignette
             */

            _swanController.SetActive(false);
            
            _cutsceneAnimator.Play("End cutscene");

            StartCoroutine(WaitForAnimationComplete());
            //Debug.Log("Load Next scene");
            
        }
    }
    
    IEnumerator WaitForAnimationComplete()
    {
        //float animationLength = _cutsceneAnimator.GetCurrentAnimatorStateInfo(0).length;
        
        yield return new WaitForSecondsRealtime(keyframes/keyframesPerSecond);
        
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }
}
