using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneryManager : MonoBehaviour, IStageComponent
{
    public bool LongPressing { get; set; }
    public bool LongRight { get; set; }

    [SerializeField] List<Sprite> forestSprites;
    [SerializeField] List<Sprite> desertSprites;
    [SerializeField] List<Sprite> citySprites;
    [SerializeField] List<Sprite> spaceSprites;

    [SerializeField] SpriteRenderer sceneSprite;

    List<Sprite> selectedSprites;
    int curSprite = 0;


    private void Awake()
    {
        selectedSprites = forestSprites;
        sceneSprite.sprite = selectedSprites[0];
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

            yield return new WaitForSeconds(0.1f);
            if (curSprite == 1)curSprite = 0;
            else curSprite = 1;
            sceneSprite.sprite = selectedSprites[curSprite];
        }
        yield return new WaitForSeconds(0.2f);
    }


}
