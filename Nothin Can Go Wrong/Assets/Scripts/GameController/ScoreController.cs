using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    [SerializeField] int vidaInicial;

    public static TextMeshProUGUI finalScoreText;

    public static TextMeshProUGUI finalDescriptionText;

    public static int curVida;
    public static int vidaAnterior;
    public static double ultimoScore;

    public static double curScore=0;

    public static Sound curSound;

    public static GameObject scoreGameO;
    public static Vector2 scoreGOriginalPos;

    public static bool isScoreOnScreen=false;


    public static GameObject mainMenuGO;


    public static void setCurSound(Sound sound){
        curSound=sound;
    }

    public static double percentageCurracy;

    void Start()
    {
        mainMenuGO = GameObject.Find("MenuCanvas");
        scoreGameO = GameObject.Find("FinalScoreBackground");
        scoreGOriginalPos = scoreGameO.transform.position;
        finalScoreText =GameObject.Find("finalScoreText").GetComponent<TextMeshProUGUI>();
        finalDescriptionText=GameObject.Find("finalDescriptionText").GetComponent<TextMeshProUGUI>();
        if(finalScoreText!=null && finalDescriptionText!=null){
        }
        curVida=vidaInicial;
    }

    public static void resetScore(){
        ultimoScore=curScore;
        curScore=0.00;
        finalDescriptionText.text = "Is just terrible.";
        SetFinalScore(0.00);
    }


    public static void atualizarVida(){
        vidaAnterior=curVida;
        if(percentageCurracy>=65)
        {
            curVida++;
        }
        else
        {
            curVida--;
        }
    }

    public static double HandleShortNotes(float distance){
        if (distance <= 0.1) 
        {
            curScore+=1;
        }
        else if (distance <= 0.3) 
        {
            curScore+=0.85;
        }
        else if (distance < 2) 
        {
            curScore+=0.65;
        }
        double percentage=curScore/curSound.notesCount*100;
        percentage=Convert.ToDouble(Math.Round((float)percentage, 2)) ;
        SetFinalScore(percentage);
        SetFinalDescription();
        return percentage;
    }

    public static double HandleLongNotes(double distance){
        
        curScore+=distance;
        return curScore/curSound.notesCount*100;
    }

    public static void  SetFinalDescription(){
        string finalDescription="";
        int pontuation=0;
        if(PlaySettingsController.specialist.preferredClothe==PlaySettingsController.choosedClothes){
            finalDescription+="I really liked the clothes. ";
            pontuation++;
        }
        else{
            finalDescription+="I really hate the clothes. ";
        }
        if(PlaySettingsController.specialist.preferredScene==PlaySettingsController.choosedScene){
            finalDescription+="I liked the scenarios. ";
            pontuation++;
        }
        else{
            finalDescription+="The scenarios are so bad. ";
        }
        if(PlaySettingsController.specialist.preferredTheme==PlaySettingsController.choosedTheme){
            finalDescription+="The plot was very interesting. ";
            pontuation++;
        }
        else{
            finalDescription+="The plot was horrible. ";
        }
        if(curScore*PlaySettingsController.ReturnPointsMultiplier()>=65){
            if(pontuation>=2){
                finalDescription+="I really loved. ";
            }
            else{
                finalDescription+="but I kind of liked it. ";
            }
        }
        else{
            if(pontuation>=2){
                finalDescription+="Despite that I hated it. ";
            }
            else{
                finalDescription+="but I hated it. ";
            }
        }
        finalDescriptionText.text=finalDescription;
    }
    public static void SetFinalScore(double score){
        
        finalScoreText.text= Convert.ToDouble(Math.Round((float)score, 2)).ToString()+"%";

    }

    public static void ActiveScoreScreen()
    {
        var sequence = DOTween.Sequence();
        if (!isScoreOnScreen)
        {
            sequence.Append(scoreGameO.transform.DOLocalMoveY(0, 2f));
            isScoreOnScreen=true;
        }
        else
        {
            sequence.Append(scoreGameO.transform.DOLocalMoveY(scoreGOriginalPos.y, 2f));
            isScoreOnScreen = false;
            sequence.onComplete = backToMenu;
                
        }
        
        sequence.Play();

    }
    public static double GetScore()
    {
        return curScore;
    }
    public static void backToMenu()
    {
        resetScore();
        mainMenuGO.SetActive(true);
    }

}
