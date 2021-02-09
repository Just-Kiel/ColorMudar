using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField] private AudioSource musicStream;
    [SerializeField] public Text musicText;
    private float musicVolume;
    
    [SerializeField] private AudioSource soundStream;
    [SerializeField] public Text soundText;
    private float soundVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this);
        }
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
