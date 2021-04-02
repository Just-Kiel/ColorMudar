using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time; //temps global
    public float stopTime; //temps au moment de relancer
    public Text champTimer = null; //texte du temps global

    public float timeLevel; //temps par niveau
    public Text champTimerLevel = null; //texte du temps par niveau

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Intro")
        {
            stopTime = PlayerPrefs.GetFloat("stopTime");
        }
        //si on est à la première scene du niveau
        if (SceneManager.GetActiveScene().name == "Intro" || SceneManager.GetActiveScene().name == "Level1_3" || SceneManager.GetActiveScene().name == "Level2_1" || SceneManager.GetActiveScene().name == "Level3_1")
        {
            //Debug.Log("Debut de niveau");
            PlayerPrefs.SetFloat("timeLevel", 0); //le temps par niveau est remis à 0
        }

        timeLevel = PlayerPrefs.GetFloat("timeLevel"); //on récupère le temps enregistré
    }

    void Update()
    {
        if (champTimer != null && champTimerLevel != null)
        {
            champTimer.text = "Temps total :" + string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60); //écriture du temps total selon le format 00:00
            

            //time = (int)Time.time; //le temps total se calcule en fonction du temps écoulé

            champTimerLevel.text = string.Format("{0:0}:{1:00}", Mathf.Floor(timeLevel / 60), timeLevel % 60);
        }
        time = (int)(Time.time - stopTime);
        timeLevel += Time.deltaTime; //temps par niveau calculé en fonction du delta
        PlayerPrefs.SetFloat("timeLevel", timeLevel); //setup de la variable avant fermeture de la scène
    }
}
