using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpecialistController : MonoBehaviour
{
    Specialist Specialist;
    [SerializeField] List<string> phrases; 
    [SerializeField] List<string> specialistPhrases; 

    [SerializeField] GameObject character1;
    [SerializeField] GameObject character2;

    InitialAnimator char1Animator,char2Animator;

    [SerializeField] GameObject cortinaEsquerda;
    [SerializeField] GameObject cortinaDireita;
    
    [SerializeField] Light2D holofote;
    [SerializeField] GameObject gameScene;
    float originalNivelLuz;

    Vector2 CortinaEsqOriginalPos;
    Vector2 CortinaDirOriginalPos;
    int curPhrase=0;
    bool endDialog=false,Opened=false;

    bool canJumpDialog=false;


    [SerializeField] SubmitAndHandleOptions submitAndHandleOptions;



    void RestartPhrases(){
        phrases = new List<string>();
        phrases.Add("Boy you wouldn't believe it if I told you.");
        phrases.Add("The great theater critic is coming to watch us next week.");
        phrases.Add("I need you to make the best play we've ever had.");
        phrases.Add("We have to impress him at all costs.");
        phrases.Add("I saw in the newspaper that he likes %");
        phrases.Add("In terms of costume design, they usually give good marks to %");
        phrases.Add("and the scenario has to be expanded, it has to be %");
        

    }


    void Awake()
    {   
        CortinaEsqOriginalPos=cortinaEsquerda.transform.position;
        CortinaDirOriginalPos=cortinaDireita.transform.position;
        originalNivelLuz=holofote.falloffIntensity;
        char1Animator=character1.GetComponent<InitialAnimator>();
        char2Animator=character2.GetComponent<InitialAnimator>();
    }
    void OnEnable()
    {
        StartCoroutine(WhenActiveObject());
        
    }

    IEnumerator WhenActiveObject()
    {
        endDialog = false;
        curPhrase = 0;
        this.Specialist = PlaySettingsController.SetupSpecialist();
        specialistPhrases.Add(this.Specialist.ThemeString);
        specialistPhrases.Add(this.Specialist.ClotheString);
        specialistPhrases.Add(this.Specialist.SceneString);
        RestartPhrases();
        submitAndHandleOptions.OnEndAction = LastDialog;
        yield return playCharsAnimation();
        if (DialogSystem.Instance != null)  DialogSystem.Instance.SetActive(true);
        yield return DialogSystem.Instance.TypeDialog(phrases[curPhrase]);
        canJumpDialog = true;
        if (SubmitAndHandleOptions.Instance != null) SubmitAndHandleOptions.Instance.submitSettings = false;

    }


    IEnumerator playCharsAnimation(bool endAnim=false){
        StartCoroutine(char1Animator.playAnimation());
        yield return char2Animator.playAnimation();
        if(endAnim){
            CortinasController.AbrirCortinas();
            gameObject.SetActive(false);
        }
    }

    IEnumerator LastDialog(){
        DialogSystem.Instance.SetActive(true);
        phrases= new List<string>();
        phrases.Add("<color=#FF0000>NOTHING</color>");
        phrases.Add("<color=#FF0000>CAN</color>");
        phrases.Add("<color=#FF0000>GO</color>");
        phrases.Add("<color=#FF0000>WRONG!</color>");
        curPhrase=0;
        yield return DialogSystem.Instance.TypeDialog(phrases[curPhrase]);
        SubmitAndHandleOptions.Instance.submitSettings=true; 
    }
    void Update()
    {        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter)) 
        && !PlaySettingsController.InGame
        && !DialogSystem.Instance.isTalking
        && canJumpDialog
        ){
            NextDialog();
        }
    }

    private void NextDialog()
    {
            curPhrase++;
            if(curPhrase<=phrases.Count-1){
                if(phrases[curPhrase].Contains("%")){
                    Debug.Log(specialistPhrases[0]);
                    phrases[curPhrase]=phrases[curPhrase].Replace("%",specialistPhrases[0]);
                    specialistPhrases.RemoveAt(0);
                }
                StartCoroutine(DialogSystem.Instance.TypeDialog(phrases[curPhrase]));
            }
            else{
                if(!endDialog){
                SubmitAndHandleOptions.Instance.SetActive(true);
                StartCoroutine(SubmitAndHandleOptions.Instance.PlayOpenAnimation());
                endDialog=true;
                }
                else{
                    if(SubmitAndHandleOptions.Instance.submitSettings)StartCoroutine(playCharsAnimation(true));
                }
                DialogSystem.Instance.SetActive(false);

            }   
    }
    

    
}


