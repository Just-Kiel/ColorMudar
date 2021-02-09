using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioSource musicStream;
    public Text musicText;
    public float musicVolume;
    
    public AudioSource soundStream;
    public Text soundText;
    public float soundVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        } else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        musicVolume = musicStream.volume * 5;
        musicText.text = musicVolume.ToString();

        soundVolume = soundStream.volume * 5;
        soundText.text = soundVolume.ToString();
    }

    public void MusicVolumeUp()
    {
        musicStream.volume += 0.2f;

        if (musicStream.volume >= 1f)
        {
            musicStream.volume = 1f;
        }
    }
    public void MusicVolumeDown()
    {

        musicStream.volume -= 0.2f;

        if (musicStream.volume <= 0.1f)
        {
            musicStream.volume = (int)0;
        }
    }
    public void SoundVolumeUp()
    {
        soundStream.volume += 0.2f;

        if (soundStream.volume >= 1f)
        {
            soundStream.volume = 1f;
        }
    }
    public void SoundVolumeDown()
    {
        soundStream.volume -= 0.2f;

        if (soundStream.volume <= 0.1f)
        {
            soundStream.volume = (int)0;
        }
    }
}
