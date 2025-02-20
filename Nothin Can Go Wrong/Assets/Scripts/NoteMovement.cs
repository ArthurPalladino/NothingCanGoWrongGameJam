using DG.Tweening;
using TMPro;
using UnityEngine;


public class NoteMovement : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    [SerializeField] public KeyCode key;
    [SerializeField] GameObject textObject;
    TMP_Text text;

    private void Awake()
    {
        text = textObject.GetComponent<TMP_Text>();
        text.text = key.ToString();
        body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        body.linearVelocity = new Vector2(-speed, 0);

        //32.9 +/-
        //tempo = 21.5 frames +/-

        transform.DORotate(new Vector3(0, 0, -5 * Random.Range(0.75f, 1.5f)), 1, RotateMode.LocalAxisAdd)
       .SetLink(this.gameObject)
       .SetLoops(-1, LoopType.Yoyo)
       .SetEase(Ease.InOutCirc);
    }

    void Update()
    {
        
    }
}
