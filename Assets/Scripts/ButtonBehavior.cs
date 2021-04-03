using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public AudioSource AudioSource; //source sonore du bouton
    public AudioClip ClickSound; //son du bouton

    [SerializeField] GameObject currentButton = null; //bouton actif

    public void Update()
    {
        AudioSource = AudioManager.instance.soundStream; //source sonore de l'audioManager
        //si il y a un bouton actif et qu'il ne l'était pas avant
        if (EventSystem.current.currentSelectedGameObject && (currentButton == null || currentButton.name != EventSystem.current.currentSelectedGameObject.name))
        {
            //on joue le son et on le garde en mémoire
            PlayClickSound();
            currentButton = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void PlayClickSound()
    {
        AudioSource.PlayOneShot(ClickSound);
    }
}
