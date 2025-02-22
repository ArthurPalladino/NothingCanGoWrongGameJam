using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class noteTimer
{
    public string note;
    public float startIndex;
    public float endIndex;
}

[System.Serializable]
public class songScript
{
    public float delay = 0;
    public float speed = 0;
    public List<noteTimer> notes = new List<noteTimer>();
}

public class NoteSpawner : MonoBehaviour
{
    float bps = 0;
    float spn = 0;
    float tempoTick = 0;
    public bool createMode;

    noteTimer curQNote;
    noteTimer curWNote;
    noteTimer curENote;
    noteTimer curRNote;

    songScript songScript = new songScript();
    float tempo = 0;


    [SerializeField] GameObject notePrefab;
    [SerializeField] GameObject longNotePrefab;


    string songName = "comedy_track";
    Sound track;




    public void SetSong(string songName)
    {
        Sound music = SoundManager.GetSong(songName);
        track = music;
        ScoreController.setCurSound(music);
        bps = music.bpm / 60f;
        spn = 1f / bps;
        tempoTick = spn / 8f;
        Debug.Log(tempoTick);
    }
    void Start()
    {   
        SetSong(songName);

    }

    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)){
            songScript.notes = new List<noteTimer>();
            createMode = true;
            track.source.Play();
            timer = 0;
        }

        if (createMode){

            if (Input.GetKeyDown(KeyCode.Q))
            {
                curQNote = RegisterNote(KeyCode.Q, timer);
            }
            if (Input.GetKeyUp(KeyCode.Q) && curQNote != null) {
                curQNote.endIndex = Mathf.RoundToInt(timer / tempoTick);
                curQNote = null;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                curWNote = RegisterNote(KeyCode.W, timer);
            }
            if (Input.GetKeyUp(KeyCode.W) && curWNote != null)
            {
                curWNote.endIndex = Mathf.RoundToInt(timer / tempoTick);
                curWNote = null;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                curENote = RegisterNote(KeyCode.E, timer);
            }
            if (Input.GetKeyUp(KeyCode.E) && curENote != null)
            {
                curENote.endIndex = Mathf.RoundToInt(timer / tempoTick);
                curENote = null;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                curRNote = RegisterNote(KeyCode.R, timer);
            }
            if (Input.GetKeyUp(KeyCode.R) && curRNote != null)
            {
                curRNote.endIndex = Mathf.RoundToInt(timer / tempoTick);
                curRNote = null;
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (createMode)
            {
                timer = 0;
                foreach (noteTimer n in songScript.notes)
                {
                    Debug.Log(n.note + " - " + n.startIndex.ToString());


                }
                string json = JsonUtility.ToJson(songScript);
                Debug.Log(json);

                File.WriteAllText(Application.dataPath + "/"+songName + "_script.json", json);
                AssetDatabase.Refresh();
                //var sr = System.IO.File.CreateText("C:\\Users\\marlo\\Downloads\\" + songName + "_script.json");
                //sr.WriteLine(json);
                //sr.Close();
            }
            else
            {
                StartCoroutine(spawnNotes_tempo());
            }
            //string songJson = JsonUtility.ToJson(songScript);
            //StartCoroutine(spawnNotes());
        }
    }

    noteTimer RegisterNote(KeyCode key, float time)
    {
        if(songScript.notes.Count <= 0)
        {
            songScript.delay = time;
            songScript.speed = 16.45f / time;
            time = 0;
            timer = 0;

        }

        noteTimer n = new noteTimer();
        n.note = key.ToString();
        n.startIndex = Mathf.RoundToInt(time / tempoTick);
        Debug.Log(n.note.ToString() + " - " + n.startIndex.ToString());
        songScript.notes.Add(n);
        return n;
    }

    IEnumerator spawnNotes()
    {
        Debug.Log(track.script.text);

        songScript = JsonUtility.FromJson<songScript>(track.script.text); 
        
        track.source.Play();
        

        tempo = 0;
        Vector2 pos = transform.position;

        //stopwatch.Start();

        float time = 0;
        while (songScript.notes.Count > 0){

            foreach (noteTimer note in songScript.notes.FindAll(x => x.startIndex-songScript.delay <= time)){
                //if (note.endIndex != note.startIndex)
                //{
                //    GameObject g = longNotePrefab;
                //    LongNoteController script = g.GetComponent<LongNoteController>();
                //    script.speed = 4;
                //    script.key = note.note;
                //    //var size =  note.endIndex - note.startIndex + 1;
                //    //script.size = size;

                //    if (note.note == KeyCode.R || note.note == KeyCode.W) pos.y = 3.273f - 0.31f;
                //    else pos.y = transform.position.y - 0.31f;

                //    Instantiate(g, new Vector2(pos.x + size / 2, pos.y), Quaternion.identity);
                //}
                //else
                //{
                    KeyCode keyNote = (KeyCode)System.Enum.Parse(typeof(KeyCode), note.note, true);
                    GameObject g = notePrefab;
                    NoteMovement script = g.GetComponent<NoteMovement>();
                    script.speed =songScript.speed;
                    script.key = keyNote;

                    if (keyNote == KeyCode.R || keyNote == KeyCode.W) pos.y = 3.273f - 0.31f;
                    else pos.y = transform.position.y - 0.31f;

                    Instantiate(g, pos, Quaternion.identity);

                //}
                songScript.notes.Remove(note);


            }

            time += Time.deltaTime;
            UnityEngine.Debug.Log(time);

            yield return new WaitForSeconds(0);
        }

    }


    IEnumerator spawnNotes_tempo()
    {
        Debug.Log(track.script.text);

        songScript = JsonUtility.FromJson<songScript>(track.script.text);

        track.source.Play();


        tempo = 0;
        Vector2 pos = transform.position;


        while (songScript.notes.Count > 0)
        {
            float time = 0;
            foreach (noteTimer note in songScript.notes.FindAll(x => x.startIndex <= tempo))
            {
                KeyCode keyNote = (KeyCode)System.Enum.Parse(typeof(KeyCode), note.note, true);
                if (keyNote == KeyCode.R || keyNote == KeyCode.W) pos.y = 3.273f - 0.31f;
                else pos.y = transform.position.y - 0.31f;

                if (note.endIndex != note.startIndex)
                {
                    GameObject g = longNotePrefab;
                    LongNoteController script = g.GetComponent<LongNoteController>();
                    script.speed = songScript.speed;
                    script.key = keyNote;
                    var size = Mathf.RoundToInt(note.endIndex - note.startIndex + 1)/2;
                    script.size = size;



                    Instantiate(g, new Vector2(pos.x + size / 2, pos.y), Quaternion.identity);
                }
                else
                {
                    
                    GameObject g = notePrefab;
                    NoteMovement script = g.GetComponent<NoteMovement>();
                    script.speed = songScript.speed;
                    script.key = keyNote;

                   
                    Instantiate(g, pos, Quaternion.identity);

                }
                songScript.notes.Remove(note);

                
            }
            tempo += 1;
            time += Time.deltaTime;

            yield return new WaitForSeconds(tempoTick - time);


        }

    }
}
