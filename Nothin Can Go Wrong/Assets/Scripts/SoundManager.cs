using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    public float bpm;
    public int notesCount = 0;
    public TextAsset script;

    [Range(0f, 1f)]
    public float volume;


    [HideInInspector]
    public AudioSource source;
}


public class SoundManager : MonoBehaviour
{
    public static Sound[] sounds;
    [SerializeField] Sound[] soundsSerialized;


    public TextAsset[] textFiles;


    void Awake()
    {
        sounds = soundsSerialized;

        foreach (var sound in sounds)
        {
            string fs = sound.script.text;

            int startIndex = fs.IndexOf("\"notes\":[");
            if(startIndex==-1) startIndex = fs.IndexOf("\"notes\": [");
            if (startIndex != -1)
            {
                startIndex += 8;
                int endIndex = fs.IndexOf("]", startIndex);

                if (endIndex != -1)
                {
                    string notesContent = fs.Substring(startIndex, endIndex - startIndex);
                    string[] noteItems = notesContent.Split(new char[] { '{' }, StringSplitOptions.RemoveEmptyEntries);
                    sound.notesCount = noteItems.Length;
                }
            }
        }
        defineSource();

    }





    public static bool isListSeted=false;
 

    void defineSource()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    public static Sound GetSong(string name)
    {   
        var s=Array.Find(sounds, sound => sound.name == name);
        s.volume = AudioController.GetGameVolume();
        s.source.volume = AudioController.GetGameVolume();
        return s;

    }


    public static void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume=AudioController.GetGameVolume();
        s.source.Play();
        
    }

    public static void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        
    }
    

}
