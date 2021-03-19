using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2DCloud; //Rigidbody du nuage (permet de influer sa gravité/ses mouvements)
    [SerializeField] private float speedMove = 200; //vitesse de déplacement

    // Update is called once per frame
    void Update()
    {
        rb2DCloud.velocity = Vector2.right * speedMove; //déplacement du nuage vers la droite
    }
}
