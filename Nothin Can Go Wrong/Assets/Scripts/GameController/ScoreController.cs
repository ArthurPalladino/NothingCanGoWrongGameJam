using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] int vidaInicial;
    public static int curVida;
    public static int vidaAnterior;
    public static double ultimoScore;

    public static double curScore=0;

    public static Sound curSound;


    public static void setCurSound(Sound sound){
        curSound=sound;
    }

    public static double percentageCurracy;
    void Start()
    {
        curVida=vidaInicial;
    }

    public static void resetScore(){
        ultimoScore=curScore;
        curScore=0;
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
        Debug.Log(percentage);
        return Convert.ToDouble(percentage);
    }

    public static double HandleLongNotes(double distance){
        
        curScore+=distance;
        return curScore/curSound.notesCount*100;
    }
}
