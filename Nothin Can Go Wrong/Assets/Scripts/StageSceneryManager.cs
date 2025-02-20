using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StageSceneryManager : MonoBehaviour, IStageComponent
{
    public bool LongPressing { get; set; }
    public bool LongRight { get; set; }


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


    }

    public IEnumerator Activate(bool right)
    {
        if (!DOTween.IsTweening(transform))
        {
            transform.DOMoveY(-2.43f, 0.2f).SetEase(Ease.InOutBounce).SetLoops(2, LoopType.Yoyo).SetLink(this.gameObject);
        }
        yield return new WaitForSeconds(0.2f);
    }


}
