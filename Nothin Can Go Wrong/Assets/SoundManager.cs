using UnityEngine;
using UnityEngine.Audio;
using System;
using System.IO;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    public float length;
    public float bpm;
    public TextAsset script;

    [Range(0f, 1f)]
    public float volume;


    [HideInInspector]
    public AudioSource source;
}


public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.length = s.clip.length;
        }
    }

    public Sound GetSong(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
        
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
