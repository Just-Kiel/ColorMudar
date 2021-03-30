using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.U2D.Animation;

public class SelectedPlayer : MonoBehaviour
{
    [SerializeField] private Transform[] PersoList;
    [SerializeField] private string currentPerso;

    // Start is called before the first frame update
    void Start()
    {
        currentPerso = MenuStart.currentPlayer;

        foreach(Transform player in PersoList)
        {
            if(currentPerso != player.gameObject.name)
            {
                player.gameObject.SetActive(false);
            }
        }
    }
}
