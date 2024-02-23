using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public delegate void PauseToggle();
    public static PauseToggle pauseToggle;
    private bool isPaused;
    public GameObject pauseMenuUI;

    [Header("First Selections")]
    [SerializeField] private GameObject pauseMenuFirst;

    // Start is called before the first frame update
    void Start()
    {
        pauseToggle += PauseActivation;
        isPaused = false; 
    }

    private void OnDestroy()
    {
        pauseToggle -= PauseActivation;
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
                Pause();
            else 
                Resume();
        } */
    }


    public void PauseActivation()
    {
        isPaused = !isPaused;
        if (isPaused)
            Pause();
        else
            Resume();
    }

    

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
       
    
    Time.timeScale = 1.0f;
    }
    
    public void MainMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
