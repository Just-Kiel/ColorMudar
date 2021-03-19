using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; //création d'une instance d'audio manager

    public AudioSource musicStream; //récupération de la source musicale
    public Text musicText; //zone de texte affichant le volume de la musique
    public int musicVolume; //variable du volume de la musique
    
    public AudioSource soundStream; //récupération de la source sonore
    public Text soundText; //zone de texte affichant le volume des sons
    public int soundVolume; //variable du volume des sons

    void Awake()
    {
        if (instance == null) //si aucune instance AudioManager existe
        {
            instance = this; //on en crée 1
            
        } else
        {
            Destroy(this.gameObject); //si + de 1 on la détruit
        }
        DontDestroyOnLoad(this.gameObject); //conservation de l'objet entre les scènes
    }

    private void Update()
    {
        musicVolume = (int) (musicStream.volume * 10); //setup du volume de musique à afficher (on multiplie par 10 la variable de l'audio source car on veut aller de 0 à 10 et non de 0 à 1)
        
        soundVolume = (int) (soundStream.volume * 10); //setup du volume de son à afficher
        
        //si les zones de texte existent, on affiche les volumes (variables int converties en chaines de caractères)
        if(musicText && soundText)
        {
            musicText.text = musicVolume.ToString();
            soundText.text = soundVolume.ToString();
        }
    }

    //fonction d'augmentation du volume de la musique
    public void MusicVolumeUp()
    {
        musicStream.volume += 0.1f;

        //si le volume est supérieur à 1 sur la source, on le force à 1 (c'est la limite)
        if (musicStream.volume >= 1f)
        {
            musicStream.volume = 1f;
        }
    }

    //fonction de réduction du volume de la musique
    public void MusicVolumeDown()
    {
        musicStream.volume -= 0.1f;

        //si le volume est inférieur à 0.1 (proche de 0) sur la source, on le force à 0 (c'est la limite)
        if (musicStream.volume <= 0.1f)
        {
            musicStream.volume = (int)0;
        }
    }

    //fonction d'augmentation du volume des sons (pareil que la musique)
    public void SoundVolumeUp()
    {
        soundStream.volume += 0.1f;

        if (soundStream.volume >= 1f)
        {
            soundStream.volume = 1f;
        }
    }

    //fonction de réduction du volume des sons (pareil que la musique)
    public void SoundVolumeDown()
    {
        soundStream.volume -= 0.1f;

        if (soundStream.volume <= 0.1f)
        {
            soundStream.volume = (int)0;
        }
    }
}
