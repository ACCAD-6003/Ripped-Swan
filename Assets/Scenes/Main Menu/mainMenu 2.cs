using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class mainMenu : MonoBehaviour
{
    public Scrollbar scrollbar;
    public TMP_Text progressText;

    public GameObject screen;
    public GameObject videoPlayer;
    public VideoPlayer player;
    public bool isLevelLoaded;

    private double start;
    public void playIntroCutscene()
    {
        screen.SetActive(true);
        videoPlayer.SetActive(true);
        player.Play();
        start = Time.time;
    }

    private void Start()
    {
        isLevelLoaded = false;
        player = videoPlayer.GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        // Player can interrupt intro scene by pressing any key
        if (Input.anyKeyDown && player.isPlaying)
        {
            player.Stop();
            screen.SetActive(false);
            videoPlayer.SetActive(false);
            LoadLevel(1);
        }
        // At the end of intro scene, load level
        if ((Time.time - start > player.length) && !isLevelLoaded)
        {
            screen.SetActive(false);
            videoPlayer.SetActive(false);
            LoadLevel(1);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        isLevelLoaded = true;
        StartCoroutine(LoadAsynchronously(sceneIndex));
        //SceneManager.LoadScene(1);
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            scrollbar.size = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
