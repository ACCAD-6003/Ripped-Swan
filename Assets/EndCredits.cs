using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndCredits : MonoBehaviour
{
    public GameObject screen;
    public VideoPlayer player;
    public GameObject canvas;

    private double start;
    private bool creditsHaveStarted;
    private bool playerStarted;

    private void Start()
    {
        start = Time.time;
        creditsHaveStarted = false;
        playerStarted = false;
    }

    private void Update()
    {
        if (player.isPlaying && !playerStarted) playerStarted = true; // sets to true once
        if (playerStarted) {
            if (!player.isPlaying)
            {
                AudioListener.pause = false;
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(0);
            }
        }
    }

    public void StartCredits()
    {
        start = Time.time;
        /*canvas.transform.Find("UI_Box").gameObject.SetActive(false);
        canvas.transform.Find("UI_PlayerHP").gameObject.SetActive(false);
        canvas.transform.Find("UI_Feathers").gameObject.SetActive(false);
        canvas.transform.Find("UI_specialAttackMenu").gameObject.SetActive(false);
        canvas.transform.Find("UI_controlInstructions").gameObject.SetActive(false);
        canvas.transform.Find("Screen").gameObject.SetActive(true); */
        AudioListener.pause = true;
        //creditsHaveStarted = true;
        Time.timeScale = 0.0f;
        player.Play();
    }

}
