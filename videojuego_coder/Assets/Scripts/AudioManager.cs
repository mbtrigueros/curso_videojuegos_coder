using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public bool dontDestroyOnLoad;

    [SerializeField] Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {

            if (instance == null)
            {
                instance = this;
            }
                else
                {
                    Destroy(gameObject);
                }

            DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;


        }

    }

    void Start()
    {

    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        return sound;
    }

    public void Soundtrack(string name)
    {
        int level = LevelManager.instance.GetLevel();

        switch (level)
        {
            case 1:
                PlaySound("SoundTrack_01");
                break;
            case 2:
                PlaySound("SoundTrack_01");
                break;
            case 3:
                PlaySound("SoundTrack_02");
                break;
            case 4:
                PlaySound("SoundTrack_01");
                break;
            case 5:
                PlaySound("SoundTrack_02");
                break;
            default:
                PlaySound("SoundTrack_01");
                break;
        }
    }


    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }

    public void StopSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }
}
