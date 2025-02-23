using DG.Tweening;
using UnityEngine;

public class Actor : MonoBehaviour
{
    bool isRunning = false;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite urban;
    [SerializeField] Sprite western;
    [SerializeField] Sprite animal;
    [SerializeField] Sprite futuristic;

    private void OnEnable()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        SetActorSprites(PlaySettingsController.choosedClothes);
            
        
    }


    public void SetActorSprites(Clothes clothesChoosed){
        if(clothesChoosed==Clothes.Futurista){
            spriteRenderer.sprite=futuristic;
        }
        else if(clothesChoosed==Clothes.Animal){
            spriteRenderer.sprite=animal;
        }
        else if(clothesChoosed==Clothes.Cowboy){
            spriteRenderer.sprite=western;
        }
        else if(clothesChoosed==Clothes.Padrao){
            spriteRenderer.sprite=urban;
        }
        
    }
    void Start()
    {
        Sequence s = DOTween.Sequence();

        s.Insert(
        0f + Random.Range(0.5f, 2f),
        transform
            .DORotate(
                new Vector3(0, 0, -7 * Random.Range(0.75f, 1.5f)), 1, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutCirc).SetLoops(-1, LoopType.Yoyo)
        );
    }

    public void Scatter()
    {
        if (!isRunning)
        {
            float originalX = transform.position.x;
            isRunning = true;
            Sequence ns = DOTween.Sequence();
            ns.Append(
            transform
                .DOMoveX(1f * Random.Range(-5f, 5f)
                   , 1)
                .SetEase(Ease.InOutCirc)
            );
            ns.Append(transform
                .DOMoveX(originalX, 1)
                .SetEase(Ease.InOutCirc)
            );
            ns.OnComplete(() => {isRunning = false;});
        }
    }

}
