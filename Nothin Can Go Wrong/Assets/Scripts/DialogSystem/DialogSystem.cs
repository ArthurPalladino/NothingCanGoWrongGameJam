using System;
using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] Image charSprite;

    [SerializeField] int letterPerSecond;

    public bool isTalking;
    [SerializeField] GameObject dialogGO;
    public static DialogSystem Instance{get;private set;}
    void Awake()
    {
        Instance=this;   
    }
    public void SetSprite(Sprite sprite){
        charSprite.sprite=sprite;
    }
    public IEnumerator TypeDialog(string dialog){
        isTalking=true;
        dialogText.text="";
        bool isSpecialChar=false;
        foreach(var letter in dialog.ToCharArray()){
            if(letter=='>') isSpecialChar=false;
            if(letter=='<' || isSpecialChar){
                dialogText.text+=letter;
                isSpecialChar=true;
            }
            if(!isSpecialChar){
            dialogText.text+=letter;
            yield return new WaitForSeconds(1f/letterPerSecond);
            }
        }
        isTalking=false;
    } 
    public void SetActive(bool active){
        dialogGO.SetActive(active);
    }

    
}
