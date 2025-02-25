using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static float audioVolume=0.5f;
    public static float dialogVolume = 0.7f;


    public static float GetGameVolume(){
        return audioVolume;
    }
    public void SetGameVolume(float value){
        audioVolume=value;
    }

    public static float GetDialogVolume()
    {
        return dialogVolume;
    }
    public void SetDialogVolume(float value)
    {
        dialogVolume = value;
    }
}
