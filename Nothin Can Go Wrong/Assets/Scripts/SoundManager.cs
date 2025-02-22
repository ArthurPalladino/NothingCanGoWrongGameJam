using UnityEngine;
using UnityEngine.Audio;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

    public TextAsset[] textFiles;


    public static bool isListSeted=false;
    void SetSoundList()
    {
        List<Sound> list = new List<Sound>();
        string path = Application.dataPath + "/Songs";
        DirectoryInfo info = new DirectoryInfo(path);
        FileInfo[] files = info.GetFiles("*.mp3");

        foreach (FileInfo file in files)
        {
            string url = "file://" + file.FullName; 

            using (WWW www = new WWW(url))
            {
                while (!www.isDone) { }

                if (string.IsNullOrEmpty(www.error))
                {
                    var nameValues = file.FullName.Split("-");
                    if (nameValues.Length > 2 && nameValues[2].Contains("comedy"))
                        //só enquanto só tem a musica de comedia, dps tirar o if^
                    {
                        Sound song = new Sound();
                        song.clip = www.GetAudioClip(false, false, AudioType.MPEG);
                        song.name = nameValues[2].Replace(".mp3", "") + "_track";
                        song.bpm = Int32.Parse(nameValues[0].Split('\\').Last());
                        song.script = textFiles.FirstOrDefault(txt => txt.name.Contains(song.name));
                        song.notesCount = song.script.text.Split('[')[1].Split("},").Length;
                        song.volume = 100;
                        list.Add(song);
                    }
                }
            }
        }
        sounds = list.ToArray();
        isListSeted = true;
        defineSource();
    }


    void defineSource(){
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }
    private void Awake()
    {
        SetSoundList();
        
    }

    public static Sound GetSong(string name)
    {   
        return Array.Find(sounds, sound => sound.name == name);

    }


    public static void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        
    }

    public static void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        
    }
    

}
