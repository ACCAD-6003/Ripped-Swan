using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class UI_Manager : MonoBehaviour
{
    public TMP_Text HP_Text;
    public TMP_Text Feathers_Text;
    public Slider health;
    public Slider feathers;
    private float HPtoDisplay;
    private Swan swan;

    private void Start()
    {
        swan = GameObject.FindWithTag("Player").GetComponent<Swan>();
    }

    // Update is called once per frame
    void Update()
    {
        HPtoDisplay = swan.healthPoints;
        int FeathersToDisplay = Swan.feathers;
        health.value = HPtoDisplay;
        feathers.value = FeathersToDisplay;

        HP_Text.text = HPtoDisplay.ToString();
        Feathers_Text.text = FeathersToDisplay.ToString();
    }
}
