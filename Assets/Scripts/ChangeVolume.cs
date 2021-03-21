using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Text localMusicText; //zone de texte du volume de musique sur la scène active
    public Text localSoundText; //zone de texte du volume des sons sur la scène active

    //affecte les zones de texte locales à celles de l'instance Audio Manager
    public void Update()
    {
        AudioManager.instance.musicText = localMusicText;
        AudioManager.instance.soundText = localSoundText;
    }

    //réduction du volume de la musique localement via appel de l'instance
    public void MusicDown()
    {
        AudioManager.instance.MusicVolumeDown();
    }

    //augmentation du volume de la musique localement via appel de l'instance
    public void MusicUp()
    {
        AudioManager.instance.MusicVolumeUp();
    }

    //réduction du volume des sons localement via appel de l'instance
    public void SoundDown()
    {
        AudioManager.instance.SoundVolumeDown();
    }

    //augmentation du volume des sons localement via appel de l'instance
    public void SoundUp()
    {
        AudioManager.instance.SoundVolumeUp();
    }
}
