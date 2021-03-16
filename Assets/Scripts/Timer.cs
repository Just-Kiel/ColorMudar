using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time;
    public Text champTimer;

    // Update is called once per frame
    void Update()
    {
        champTimer.text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
        time = (int)Time.time;
    }
}
