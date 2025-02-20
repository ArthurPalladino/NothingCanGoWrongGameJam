using UnityEngine;
using DG.Tweening;

public class AudienceManager : MonoBehaviour
{
    [SerializeField] GameObject[] AudienceMembers;
    [SerializeField] GameObject Critic;
    void Start()
    {

        foreach (GameObject member in AudienceMembers)
        {
            Sequence s = DOTween.Sequence();
            s.Insert(
            0f,
            member.transform
                .DORotate(
                    new Vector3(0, 0, -10 * Random.Range(0.75f, 1.5f)), 1, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutCirc)
            );
            
            s.Insert(
            0f + Random.Range(0f, 1.8f),
            member.transform
                .DOScaleY(1 * Random.Range(0.8f, 1.2f), 1)
                .SetEase(Ease.InOutCirc)
            );

            s.SetLoops(-1, LoopType.Yoyo);

            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
