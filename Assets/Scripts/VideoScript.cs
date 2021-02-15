using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_videoPlayer;

    [SerializeField] private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        m_videoPlayer.loopPointReached += VideoOver;
    }

    void VideoOver(UnityEngine.Video.VideoPlayer vp)
    {
        if (SceneManager.GetActiveScene().name == "SplashScene")
        {
            StartCoroutine(sceneLoader.LoadLevel("FirstCinematiqueScene"));
            //SceneManager.LoadScene("FirstCinematiqueScene");
        }
        
        if (SceneManager.GetActiveScene().name == "FirstCinematiqueScene")
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
