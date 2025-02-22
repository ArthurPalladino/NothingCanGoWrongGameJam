using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class SubmitAndHandleOptions : MonoBehaviour
{
    [SerializeField] PlayOption themeOption;
    [SerializeField] PlayOption clothesOption;
    [SerializeField] PlayOption sceneOption;
    
    [SerializeField] Button submitButton;

    [SerializeField] GameObject formGameObject;


    public Func<IEnumerator> OnEndAction;
    Vector2 originalPos;
    public bool isPlayingMusic;

    public Sound curMusic;

    public static SubmitAndHandleOptions Instance{get;private set;}

    public bool submitSettings=false;
    void Awake()
    {
        Instance=this;   
        
    }
    void Start()
    {
        originalPos=formGameObject.transform.localPosition;
        submitButton.onClick.AddListener(handleSubmit);
    }
    void handleSubmit(){
        StartCoroutine(SubmitSettings());
    }


    public void playDemoSong(){
        if(!isPlayingMusic){
            StartCoroutine(playSong());
        }
    }
    IEnumerator playSong(){
        Sound song = SoundManager.GetSong(themeOption.options[themeOption.curOption].song_name);
        Debug.Log(themeOption.options[themeOption.curOption].Description);
        if(song!=null){
            isPlayingMusic=true;
            curMusic=song;
            SoundManager.Play(song.name);
            yield return new WaitForSeconds(10);
            if(curMusic!=null){
            SoundManager.Stop(song.name);
            isPlayingMusic=false;
            curMusic=null;
            }
        }
        
    }
    
    IEnumerator SubmitSettings(){
        PlaySettingsController.choosedClothes=(Clothes)clothesOption.getSelectedOption();
        PlaySettingsController.choosedTheme=(Themes)themeOption.getSelectedOption();
        PlaySettingsController.choosedScene=(Scenes)sceneOption.getSelectedOption();
        yield return PlayCloseAnimation();
        if (OnEndAction != null){
            yield return OnEndAction();        
        }
    }
    IEnumerator PlayCloseAnimation(){
        var sequence= DOTween.Sequence();
        sequence.Append(formGameObject.transform.DOLocalMoveY(originalPos.y,1f));
        yield return new WaitForSeconds(1);
        SetActive(false);
    }

    public void SetActive(bool active){
        formGameObject.SetActive(active); 
    }

    

    public IEnumerator PlayOpenAnimation(){
        var sequence = DOTween.Sequence();
        formGameObject.transform.localPosition=new Vector2(formGameObject.transform.localPosition.x,originalPos.y);
        sequence.Append(formGameObject.transform.DOLocalMoveY(0,1f));
        yield return new WaitForSeconds(1);
    }

    
}
