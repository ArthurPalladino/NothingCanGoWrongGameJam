using UnityEngine;

public static class PlaySettingsController
{
    public static Specialist specialist {get;set;}
    public static Clothes choosedClothes {get;set;}
    public static Themes choosedTheme {get;set;}
    public static Scenes choosedScene {get;set;}

    public static bool InGame=false;
    public static Specialist SetupSpecialist(){
        Specialist specialistToReturn = new Specialist();
        specialistToReturn.Setup();
        specialist=specialistToReturn;
        return specialistToReturn;

    } 

    public static double ReturnPointsMultiplier(){
        double multiplier=1;
        if(choosedClothes==specialist.preferredClothe){
            multiplier+=0.5;
        }
        if(choosedScene==specialist.preferredScene){
            multiplier+=0.5;
        }
        if(choosedTheme==specialist.preferredTheme){
            multiplier+=1;
        }
        return multiplier;
    }

    
}
