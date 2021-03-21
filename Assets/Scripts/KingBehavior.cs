using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject DiscussionBox; //objet de boite de dialogue

    [SerializeField] private Rigidbody2D rb2DKing; //Rigidbody du Roi
    [SerializeField] private int speedMove = 100; //vitesse de déplacement

    // Update is called once per frame
    void Update()
    {
        if (DiscussionBox == null || DiscussionBox.activeSelf == false) //si il n'y a pas d'objet de boite de dialogue ou qu'il est désactivé
        {
            rb2DKing.velocity = Vector2.right * speedMove; //déplacement du roi vers la droite
        }
    }
}
