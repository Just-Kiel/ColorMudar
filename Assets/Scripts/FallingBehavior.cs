using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2DFall; //rigidbody des plateformes tombantes
    [SerializeField] private bool Fall; //booléen définissant la chute ou non de l'élément
    void Start()
    {
        rb2DFall = GetComponent<Rigidbody2D>(); //récupération du rigidbody correspondant à l'objet auquel le script est attribué
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //si collision avec le Player le booléen passe à vrai
        if (collision.gameObject.tag == "Player")
        {
            Fall = true;
        }
    }

    private void Update()
    {
        //si le booléen est vrai
        if(Fall == true)
        {
            //on enclenche la chute de l'objet et destruction au bout de 2 secondes
            rb2DFall.isKinematic = false;
            Destroy(gameObject, 2f);
        }
    }
}
