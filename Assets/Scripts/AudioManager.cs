using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Make sure this object does not destroy itself when loading between scenes
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(string name, float audioVol)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.volume = audioVol;
    }

    public bool IsPlaying(string name) //Checks to see if the audio track is still playing
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }//end of isPlaying

    public void ChangeVolume(string name, float audioVol)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = audioVol;
    }

    public void Pause(string name) //Pauses audio
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void Resume(string name) //Resumes audio that was paused
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.UnPause();
    }

    public void Stop(string name) //Stops a sound
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
