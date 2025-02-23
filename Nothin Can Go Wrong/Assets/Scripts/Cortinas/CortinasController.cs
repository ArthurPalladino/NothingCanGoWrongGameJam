using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CortinasController : MonoBehaviour
{
    [SerializeField] GameObject serializedcortinaEsquerda;
    [SerializeField] GameObject serializedcortinaDireita;
    [SerializeField] GameObject serializedgameScene;
    [SerializeField] Light2D serializedHolofote;

    public static bool Opened=false;
    static GameObject cortinaEsquerda;
    static GameObject cortinaDireita;
    static GameObject gameScene;
    static Light2D Holofote;

    static Vector2 CortinaEsqOriginalPos,CortinaDirOriginalPos;
    static float originalNivelLuz;


    void Start()
    {
        cortinaEsquerda=serializedcortinaEsquerda;
        cortinaDireita=serializedcortinaDireita;
        gameScene=serializedgameScene;
        Holofote=serializedHolofote;
        CortinaEsqOriginalPos=cortinaEsquerda.transform.localPosition;
        CortinaDirOriginalPos=cortinaDireita.transform.localPosition;
        originalNivelLuz=Holofote.shapeLightFalloffSize;


    }


    public static void AbrirCortinas(){
        var sequence= DOTween.Sequence();
        if(!Opened){
            sequence.Append(cortinaEsquerda.transform.DOLocalMoveX(-14,2f))
            .Join(cortinaDireita.transform.DOLocalMoveX(14,2f))
            .Join(DOTween.To(() => Holofote.shapeLightFalloffSize, x => Holofote.shapeLightFalloffSize = x, 100, 2f));
        }
        else{
            sequence.Append(cortinaEsquerda.transform.DOLocalMoveX(CortinaEsqOriginalPos.x,2f))
            .Join(cortinaDireita.transform.DOLocalMoveX(CortinaDirOriginalPos.x,2f))
            .Join(DOTween.To(() => Holofote.shapeLightFalloffSize, x => Holofote.shapeLightFalloffSize = x, originalNivelLuz, 2f));        
        }
        sequence.Play();
        Opened=!Opened;
        if(Opened){
            gameScene.SetActive(true);
            PlaySettingsController.InGame=true;
        }
        else{
            sequence.onComplete=DisableGameScene;
            
        }
        //yield return new WaitForSeconds(sequence.Duration(true));
    
    }
    public static void DisableGameScene(){
        gameScene.SetActive(false);
        PlaySettingsController.InGame=false;
        ScoreController.ActiveScoreScreen();

    }



}
