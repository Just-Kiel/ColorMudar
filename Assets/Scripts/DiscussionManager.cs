using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionManager : MonoBehaviour
{
    public GameObject DiscussionBox; //objet contenant les éléments de narration (boites de dialogues, explications...)

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetButtonDown("Horizontal")) //si n'importe quelle touche est activée (à part le déplacement latéral)
        {
            DiscussionBox.SetActive(false); //on désactive l'objet
        }
    }
}
