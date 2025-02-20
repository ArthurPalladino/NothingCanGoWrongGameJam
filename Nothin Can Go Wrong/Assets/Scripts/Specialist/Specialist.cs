
using UnityEngine;

public class Specialist
{

    public Themes preferredTheme {get;private set;}
    public Scenes  preferredScene{get;private set;}
    public Clothes preferredClothe {get;private set;}

    public string ThemeString {get;private set;}
    public string SceneString {get;private set;}
    public string ClotheString {get;private set;}

    public void Setup()
    {
        
        preferredTheme = (Themes)Random.Range(0, Themes.GetValues(typeof(Themes)).Length);
        preferredScene = (Scenes)Random.Range(0, Scenes.GetValues(typeof(Scenes)).Length);
        preferredClothe = (Clothes)Random.Range(0, Clothes.GetValues(typeof(Clothes)).Length);
        ThemeString=GetRandomThemeWord();
        SceneString=GetRandomSceneWord();
        ClotheString=GetRandomClotheWord();
    }


    private string GetRandomThemeWord(){
        string messageToReturn="";
        //COMÉDIA
        if((int)preferredTheme==0){
            messageToReturn="<color=#FF0000> to have a good laugh</color>";

        }
        //TERROR
        else if((int)preferredTheme==1){
            messageToReturn="<color=#FF0000> crapping his pants</color>";

        }
        //ROMANCE
        else if((int)preferredTheme==2){
            messageToReturn="<color=#FF0000> sweet and dramatic</color> things";
        }
        return messageToReturn;
    }
    private string GetRandomSceneWord(){
        string messageToReturn="";
        //espacial
        if((int)preferredScene==0){
            messageToReturn="something <color=#0013ff>outside the earth</color>";
        }
        //deserto
        else if((int)preferredScene==1){
            messageToReturn="something more <color=#0013ff> cowboy and old west</color>";

        }
        //floresta
        else if((int)preferredScene==2){
            messageToReturn="something more like<color=#0013ff> tarzan or jumanji</color>";
        }
        //cidade
        else if((int)preferredScene==3){
            messageToReturn="something more<color=#0013ff> urban and common</color>";
        }
        return messageToReturn;
    }
    private string GetRandomClotheWord(){
        string messageToReturn="";
        
        //futurista
        if((int)preferredTheme==0){
            messageToReturn="<color=#088900> robots and technological things</color>";
        }
        //animal
        else if((int)preferredTheme==1){
            messageToReturn="weird things like<color=#088900> furries</color>";

        }
        //cowboy
        else if((int)preferredTheme==2){
            messageToReturn="<color=#088900> the good old western fashion</color>";
        }
        //padrao
        else if((int)preferredTheme==3){
        messageToReturn="<color=#088900> normal, boring everyday things</color>";
        }
        return messageToReturn;
    }

}


