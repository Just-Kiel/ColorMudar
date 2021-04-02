using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class FioleBehavior : MonoBehaviour
{
    [SerializeField] private DiscussionManager discuss; //appel du script de gestion d'affichage des boites de dialogue
    [SerializeField] private PlayerBehavior player; //appel du script de gestion du joueur
    [SerializeField] private ChangeColor changeColor; //appel du script de changement de couleur de perso (sprite library asset)

    [SerializeField] private SpriteLibraryAsset libraryAsset; //nouvelle sprite library asset

    private void Start()
    {
        changeColor = player.perso.GetComponent<ChangeColor>(); //récupération du script sur le player actif

        libraryAsset = changeColor.newLibraryAsset; //attribution de la nouvelle library à mettre en place
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si collision entre le player (et la fiole)
        if(collision.gameObject.tag == "Player")
        {
            player.animator.SetBool("isRunning", false); //désactivation de l'animation de course du personnage
            player.horizontalMove = 0; //arrêt de mouvement latéral du personnage
            //Debug.Log("Fiole récupérée");
            discuss.DiscussionBox.SetActive(true); //affichage de la boite de dialogue
            changeColor.Replace(libraryAsset); //changement de la couleur (library asset) du personnage
        }
    }
}
