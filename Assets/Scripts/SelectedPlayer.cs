using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.U2D.Animation;

public class SelectedPlayer : MonoBehaviour
{
    [SerializeField] private Transform[] PersoList; //tableau des personnages jouables
    [SerializeField] private string currentPerso; //personnage sélectionné

    void Start()
    {
        currentPerso = MenuStart.currentPlayer; //récupération du personnage cliqué sur le menu de sélection

        //comparaison du nom de chaque objet du tableau avec currentPerso et on désactive tous les objets non correspondants
        foreach(Transform player in PersoList)
        {
            if(currentPerso != player.gameObject.name)
            {
                player.gameObject.SetActive(false);
            }
        }
    }
}
