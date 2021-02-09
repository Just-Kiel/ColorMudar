using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Text localMusicText;
    public Text localSoundText;

    private void Update()
    {
        AudioManager.instance.musicText = localMusicText;
        AudioManager.instance.soundText = localSoundText;
    }
    public void MusicDown()
    {
        AudioManager.instance.MusicVolumeDown();
        
    }
    public void MusicUp()
    {
        AudioManager.instance.MusicVolumeUp();
    }
    public void SoundDown()
    {
        AudioManager.instance.SoundVolumeDown();
    }
    public void SoundUp()
    {
        AudioManager.instance.SoundVolumeUp();
    }
}
