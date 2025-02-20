using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayOption : MonoBehaviour
{
    [SerializeField] Image optionSprite;
    [SerializeField] Button rightButton;
    [SerializeField] Button leftButton;
    [SerializeField] TextMeshProUGUI optionText;

    [SerializeField] List<Option> options;

    [SerializeField] OptionsEnum optionType;

    int curOption=0;

    void Start()
    {
        HandleOption();
        rightButton.onClick.AddListener(delegate{NextOption(true);});
        leftButton.onClick.AddListener(delegate{NextOption(false);});
    }
    public void NextOption(bool toRight){
        if(toRight){
            curOption++;
        }
        else{
            curOption--;
        }
        if (curOption<0){
            curOption=options.Count-1;
        }
        else if (curOption>options.Count-1){
            curOption=0;
        }
        HandleOption();
    }

    void HandleOption(){
        optionSprite.sprite=options[curOption].Sprite;
        optionText.text=options[curOption].Description;
    }

    public int getSelectedOption(){
        return curOption;
    }

}

[Serializable]
public class Option{
    [SerializeField] public Sprite Sprite;
    [SerializeField] public string Description;

}
