using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private SpriteLibrary spriteLibrary = default;
    [SerializeField] private SpriteResolver spriteResolver;

    public void Replace(SpriteLibraryAsset libraryAsset)
    {
        //Debug.Log("aled");
        spriteLibrary.spriteLibraryAsset = libraryAsset;
        //Debug.Log(GameObject.FindGameObjectWithTag("Couleur").name);
        Destroy(GameObject.FindGameObjectWithTag("Couleur"));
        //spriteResolver.ResolveSpriteToSpriteRenderer();
    }
}
