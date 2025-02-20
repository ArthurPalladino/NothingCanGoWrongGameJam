using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] public static double audioVolume=100f;

    public static double GetGameVolume(){
        return audioVolume;
    }
    public void SetGameVolume(float value){
        audioVolume=value;
    }
}
