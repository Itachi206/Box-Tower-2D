using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
   
    //public float volume = 0.1f;

    public AudioSource sfx;
    public AudioSource backgroundMusic;

    public SoundsClass[] arrayOfSound;

    public Slider musicVol;
    public Slider sfxVol;

    private bool IsMute = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(SoundEnum.BackGroundMusic);
        musicVol.value = 0.5f;
        sfxVol.value = 0.5f;
    }

    public void Mute(bool _status)
    {
        IsMute = _status;
    }

    private void Update()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        sfx.volume = sfxVol.value;
        backgroundMusic.volume = musicVol.value;            
    }

    public void PlayMusic(SoundEnum soundTemp)
    {
        if (IsMute) { return; }

        AudioClip clip = GetSoundClip(soundTemp);
        if(clip != null)
        {
            backgroundMusic.clip = clip;
            backgroundMusic.Play();
        }
        else
        {
            Debug.Log("Music not Found");
        }
    }

    public void PlayEffect(SoundEnum soundTemp)
    {
        if (IsMute) { return; }

        AudioClip clip = GetSoundClip(soundTemp);
        if(clip != null)
        {
            sfx.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Sound Effect Not Found");
        }
    }

    private AudioClip GetSoundClip(SoundEnum soundTemp)
    {
       SoundsClass s = Array.Find(arrayOfSound, item => item.soundName == soundTemp);
        if (s != null)
        {
            return s.audioClip;
        }
        else
        {
            Debug.Log("The AudioClip Not Found");
            return null;
        }       
    }
 }

[Serializable]
 public class SoundsClass
{
    public SoundEnum soundName;
    public AudioClip audioClip;
}
 

public enum SoundEnum
{
     BackGroundMusic,
     ButtonClick,
     BoxCollision,
     BoxCollideWithLazer,
     GameOver
}