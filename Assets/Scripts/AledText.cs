using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AledText : MonoBehaviour
{
    public Text Collectable;

    public static int score;

    // Update is called once per frame
    void Update()
    {
        Collectable.text = "" + score;
    }
}
