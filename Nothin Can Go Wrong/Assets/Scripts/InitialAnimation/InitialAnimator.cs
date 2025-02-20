using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine.Playables;

[Serializable]
public class InitialAnimator : MonoBehaviour
{

    [SerializeField] bool isJoiningOnScene, isGoing=true;
    public float moveDistance = 5f; 
    public float jumpHeight = 0.5f; 
    public float jumpsDuration = 0.3f; 
    public int jumps = 10;      
    Vector2 originalScale;

    
    bool Opened=false;

    void Start()
    {
        originalScale=transform.localScale;
    }

    public IEnumerator playAnimation(){
        if(isGoing){
            transform.localScale=originalScale;
        }
        else{
            transform.localScale= new Vector2(originalScale.x*-1,originalScale.y);
        }
        if(isJoiningOnScene){
            yield return JumpRightAnimation();

        }
        else{
            yield return JumpLeftAnimation();
        }
        isJoiningOnScene=!isJoiningOnScene;
        isGoing=!isGoing;
    }

    IEnumerator JumpLeftAnimation()
    {
        float distancePerJump = moveDistance / jumps;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < jumps; i++)
        {
            Vector3 nextPosition = transform.position - new Vector3(distancePerJump * (i + 1), 0, 0);

            sequence.Append(transform.DOMoveY(transform.position.y - jumpHeight, jumpsDuration / 2).SetEase(Ease.OutQuad));
            sequence.Append(transform.DOMoveY(transform.position.y, jumpsDuration / 2).SetEase(Ease.InQuad));
            sequence.Join(transform.DOMoveX(nextPosition.x, jumpsDuration).SetEase(Ease.Linear));
        }

        sequence.Play();    
        yield return new WaitForSeconds(sequence.Duration(true));
        }

    IEnumerator JumpRightAnimation(float distanceToLeave=0)
    {
        float distancePerJump = moveDistance / jumps;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < jumps; i++)
        {
            Vector3 nextPosition = transform.position + new Vector3(distancePerJump * (i + 1), 0, 0);

            sequence.Append(transform.DOMoveY(transform.position.y + jumpHeight, jumpsDuration / 2).SetEase(Ease.OutQuad));
            sequence.Append(transform.DOMoveY(transform.position.y, jumpsDuration / 2).SetEase(Ease.InQuad));
            sequence.Join(transform.DOMoveX(nextPosition.x, jumpsDuration).SetEase(Ease.Linear));
        }

        sequence.Play();
        yield return new WaitForSeconds(sequence.Duration(true));
        

    }

    
}




