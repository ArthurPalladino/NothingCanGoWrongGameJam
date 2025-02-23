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

    [SerializeField] public List<Option> options;

    [SerializeField] OptionsEnum optionType;

    public int curOption=0;

    void Start()
    {
        HandleOption();
        rightButton.onClick.AddListener(delegate{NextOption(true);});
        leftButton.onClick.AddListener(delegate{NextOption(false);});
    }
    public void NextOption(bool toRight){
        if(SubmitAndHandleOptions.Instance.isPlayingMusic){
            SoundManager.Stop(SubmitAndHandleOptions.Instance.curMusic.name);
            SubmitAndHandleOptions.Instance.isPlayingMusic=false;
            SubmitAndHandleOptions.Instance.curMusic=null;
        }
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
        Debug.Log(curOption);
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
    [SerializeField] public string? song_name;

    [SerializeField] public string Description;

}
