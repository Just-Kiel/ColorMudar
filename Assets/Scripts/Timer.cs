using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time;
    public Text champTimer;

    public float timeLevel;
    public Text champTimerLevel;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Intro" || SceneManager.GetActiveScene().name == "Level1_3" || SceneManager.GetActiveScene().name == "Level2_1" || SceneManager.GetActiveScene().name == "Level3_1")
        {
            Debug.Log("Debut de niveau");
            PlayerPrefs.SetFloat("timeLevel", 0);
        }

        timeLevel = PlayerPrefs.GetFloat("timeLevel");
    }

    // Update is called once per frame
    void Update()
    {
        champTimer.text = "Temps total :" + string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
        time = (int)Time.time;

        champTimerLevel.text = string.Format("{0:0}:{1:00}", Mathf.Floor(timeLevel / 60), timeLevel % 60);
        timeLevel += Time.deltaTime;
        PlayerPrefs.SetFloat("timeLevel", timeLevel);
    }
}
