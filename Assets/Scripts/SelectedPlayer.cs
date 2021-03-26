using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.U2D.Animation;

public class SelectedPlayer : MonoBehaviour
{
    [SerializeField] private Transform[] PersoList;
    [SerializeField] private string currentPerso;

    [SerializeField] private SpriteLibrary spriteLibrary = default;
    [SerializeField] private SpriteLibraryAsset libraryAssetChoosen;

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

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Couleur")
        {
            Debug.Log("pute");
            //Replace(libraryAssetChoosen);
            
            //spriteLibrary.spriteLibraryAsset = libraryAssetChoosen;
            //Replace(libraryAssetChoosen);
        }
    }*/
    /*public void Replace(SpriteLibraryAsset libraryAsset)
    {
        Debug.Log("aled");
        spriteLibrary.spriteLibraryAsset = libraryAsset;
    }*/
}
