using System.Collections;
using UnityEngine;

public class NoteCollider : MonoBehaviour
{
    [SerializeField] public KeyCode key1;
    [SerializeField] public KeyCode key2;

    private bool hasNote = false;
    NoteMovement note;

    private bool hasLongNote = false;
    LongNoteController longNote;
    float LongNoteTimer = 0;
    float LongNoteHolder = 0;

    SpriteRenderer spriteRenderer;

    [SerializeField] public GameObject Key1_StageComponentObject;
    IStageComponent key1_stageComponent;

    [SerializeField] public GameObject Key2_StageComponentObject;
    IStageComponent key2_stageComponent;

    private float holdTimer = 0;
    private bool keyDown = false;
    private KeyCode clickedKey = KeyCode.None;



    private void Awake()
    {
        key1_stageComponent = Key1_StageComponentObject.GetComponent<IStageComponent>();
        key2_stageComponent = Key2_StageComponentObject.GetComponent<IStageComponent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (hasLongNote) LongNoteTimer += Time.deltaTime;

        if (keyDown){
            holdTimer += Time.deltaTime;
            if (hasLongNote) {
                LongNoteHolder += Time.deltaTime;
            }
            if(holdTimer > 0.25f)
            {
                
                if(clickedKey == key1) StartCoroutine(key1_stageComponent.LongPress());
                else if(clickedKey == key2) StartCoroutine(key2_stageComponent.LongPress());
            }
         }

         if(Input.GetKeyDown(key1)){
            keyDown = true;
            if(clickedKey == key2) { holdTimer = 0; key1_stageComponent.LongPressing = false; key2_stageComponent.LongPressing = false; }
            clickedKey = key1;
         }
         if(Input.GetKeyDown(key2)){
            keyDown = true;
            if (clickedKey == key1) { holdTimer = 0; key1_stageComponent.LongPressing = false; key2_stageComponent.LongPressing = false; }
            clickedKey = key2;
         }

         if (
            (Input.GetKeyUp(key1) && clickedKey == key1) 
            ||(Input.GetKeyUp(key2) && clickedKey == key2)
         ){
            keyDown = false;
            key1_stageComponent.LongPressing = false;
            key2_stageComponent.LongPressing = false;


            if (hasNote && note.key == clickedKey && hasLongNote == false)
            {
                Debug.Log("click");

                if(clickedKey == key1)StartCoroutine(key1_stageComponent.Activate(true));
                else if(clickedKey == key2)StartCoroutine(key2_stageComponent.Activate(true));

                float distance = Mathf.Abs(this.transform.position.x - note.transform.position.x) / spriteRenderer.size.x;
                StartCoroutine(ColorFeedback(distance));
                Destroy(note.gameObject);
                note = null;
                hasNote = false;
            }
            else if(holdTimer > 0.25f)
            {
                Debug.Log("longNote");
                Debug.Log(LongNoteTimer + "-" + LongNoteHolder);
                LongNoteTimer = 0;
                LongNoteHolder = 0;
            }
            else
            {
                StartCoroutine(ColorFeedback(2));
                if(clickedKey == key1)StartCoroutine(key1_stageComponent.Activate(false));
                else if(clickedKey == key2) StartCoroutine(key2_stageComponent.Activate(false));
            }


           

            clickedKey = KeyCode.None;
            holdTimer = 0;
         }

       

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note") {
            note = collision.gameObject.GetComponent<NoteMovement>(); 
            hasNote = true;
        }

        if (collision.gameObject.tag == "LongNote")
        {
            longNote = collision.gameObject.GetComponent<LongNoteController>();
            hasLongNote = true;
            LongNoteTimer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note") hasNote = false;
        if (collision.gameObject.tag == "LongNote") { hasLongNote = false; } 
        Destroy(collision.gameObject);
    }

    IEnumerator ColorFeedback(float distance)
    {
        if (distance <= 0.1) spriteRenderer.color = Color.green;
        else if (distance <= 0.3) spriteRenderer.color = Color.blue;
        else if (distance < 2) spriteRenderer.color = Color.yellow;
        else spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
