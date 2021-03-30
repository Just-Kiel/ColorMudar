using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private SpriteLibrary spriteLibrary = default; //spriteLibrary => permet de définir le set de membres du corps qu'on veut afficher
    [SerializeField] public SpriteLibraryAsset newLibraryAsset; //nouvelle sprite library asset
    //fonction permettant le remplacement de corps au contact de la fiole de couleur
    public void Replace(SpriteLibraryAsset libraryAsset)
    {
        //Debug.Log("Couleur modifiée !");
        spriteLibrary.spriteLibraryAsset = libraryAsset; //changement de la sprite library asset utilisée par celle voulue pour changer le corps
        Destroy(GameObject.FindGameObjectWithTag("Couleur")); //destruction de la fiole
    }
}
