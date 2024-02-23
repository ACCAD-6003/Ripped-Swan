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

    // Update is called once per frame
    void Update()
    {
        float HPtoDisplay = GameObject.FindWithTag("Player").GetComponent<Swan>().healthPoints;
        int FeathersToDisplay = Swan.feathers;
        health.value = HPtoDisplay;
        feathers.value = FeathersToDisplay;

        HP_Text.text = HPtoDisplay.ToString();
        Feathers_Text.text = FeathersToDisplay.ToString();
    }
}
