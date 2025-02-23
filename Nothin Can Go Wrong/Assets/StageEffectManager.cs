using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffectManager : MonoBehaviour, IStageComponent
{
    public bool LongPressing { get; set; }
    public bool LongRight { get; set; }

    public bool isPlaying = false;

    [SerializeField] ParticleSystem p;



    private void Awake()
    {
        
    }

    void Update()
    {

    }

    public IEnumerator LongPress()
    {
        LongPressing = true;
        yield return new WaitUntil(() => LongPressing == false);
        StartCoroutine(Activate(true));


    }

    public IEnumerator Activate(bool right)
    {
        if(!isPlaying){
            p.Play();
            isPlaying = true;
        }

        yield return new WaitForSeconds(1f);
        isPlaying = false;
        //p.Stop();
    }


}