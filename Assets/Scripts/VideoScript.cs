using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        m_videoPlayer.loopPointReached += VideoOver;
    }

    void VideoOver(UnityEngine.Video.VideoPlayer vp)
    {
        //vp.Stop();
        SceneManager.LoadScene("StartScene");
    }
}
