using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_videoPlayer; //ibjet permettant de lancer des vidéos

    [SerializeField] private SceneLoader sceneLoader; //appel du script permettant les écrans de chargement

    void Start()
    {
        m_videoPlayer.loopPointReached += VideoOver; //quand la vidéo est finie on appelle la fonction VideoOver
    }

    void VideoOver(UnityEngine.Video.VideoPlayer vp)
    {
        //si on se trouve sur le Splash Screen on charge avec temps de chargement la première cinématique
        if (SceneManager.GetActiveScene().name == "SplashScene")
        {
            StartCoroutine(sceneLoader.LoadLevel("FirstCinematiqueScene"));
        }
        
        //si on est sur la scene de cinématique ou des crédits on retourne à l'écran start
        if (SceneManager.GetActiveScene().name == "FirstCinematiqueScene" || SceneManager.GetActiveScene().name == "EndScene")
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
