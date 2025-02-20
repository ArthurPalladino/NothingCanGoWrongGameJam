using DG.Tweening;
using TMPro;
using UnityEngine;

public class LongNoteController : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] Canvas canvas;
    public float speed;
    public int size;
    [SerializeField] public KeyCode key;
    [SerializeField] GameObject textObject;
    TMP_Text text;

    private void Awake()
    {
        text = textObject.GetComponent<TMP_Text>();
        body = GetComponent<Rigidbody2D>();
       
    }
    void Start()
    {
        text.text = key.ToString();

        capsuleCollider.size = new Vector2(size, capsuleCollider.size.y);
        spriteRenderer.size = new Vector2(size, spriteRenderer.size.y);
        canvas.transform.localPosition = new Vector3(0 - (size/2), 0 , 0);
        body.linearVelocity = new Vector2(-speed, 0);

        transform.DORotate(new Vector3( -5 * Random.Range(0.75f, 1.5f), 0, 0), 1, RotateMode.LocalAxisAdd)
       .SetLink(this.gameObject)
       .SetLoops(-1, LoopType.Yoyo)
       .SetEase(Ease.InOutCirc);
    }

    void Update()
    {

    }
}
