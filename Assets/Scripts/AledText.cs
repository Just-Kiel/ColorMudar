using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AledText : MonoBehaviour
{
    public Text Collectable; //zone de texte où s'affiche le score

    public static int score; //nombre de collectables récupérés

    // Update is called once per frame
    void Update()
    {
        Collectable.text = "" + score; //setup du texte en fonction du score
    }
}
