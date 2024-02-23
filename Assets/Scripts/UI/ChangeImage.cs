using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
   private Image displayedImage;
    [SerializeField] private Sprite grey;
    [SerializeField] private Sprite color;
    enum ItemType { GROW, SPECIAL, HEAL}
    [SerializeField] private ItemType itemType;
    private int threshHold = 0;

    // Start is called before the first frame update
    void Start()
    {
        displayedImage = GetComponent<Image>();
        switch (itemType){
            case ItemType.GROW:
                threshHold = Swan.growCap;
                break;
            case ItemType.SPECIAL:
                threshHold = Swan.specialCap;
                break;
            case ItemType.HEAL:
                threshHold = Swan.healCap;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Swan.feathers < threshHold)
            displayedImage.overrideSprite = grey;
        else
            displayedImage.overrideSprite = color;
    }
}
