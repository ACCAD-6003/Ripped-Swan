using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{
    public Scrollbar scrollbar;
    public TMP_Text progressText;

    public GameObject screen;
    public GameObject videoPlayer;
    public VideoPlayer player;
    public bool isLevelLoaded;
    private bool inCutscene;
    private double start;

    [Header("First Selections")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingMenuFirst;

    public void playIntroCutscene()
    {
        screen.SetActive(true);
        videoPlayer.SetActive(true);
        player.Play();
        start = Time.time;
        inCutscene = true;
    }

    private void Start()
    {
       player = videoPlayer.GetComponent<VideoPlayer>();
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    private void OnEnable()
    {
        inCutscene = false;
        isLevelLoaded = false;
        player = videoPlayer.GetComponent<VideoPlayer>();
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);

    }

    private void Update()
    {

        if (inCutscene)
            CutSceneChecker();
    }


    private void CutSceneChecker()
    {
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

    public void GoToSettings()
    {
        EventSystem.current.SetSelectedGameObject(settingMenuFirst);
    }

    public void BackToMain()
    {
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }
}
    

