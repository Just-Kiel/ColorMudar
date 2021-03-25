using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2DFall;
    [SerializeField] private bool Fall;
    void Start()
    {
        rb2DFall = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fall = true;
        }
    }

    private void Update()
    {
        if(Fall == true)
        {
            rb2DFall.isKinematic = false;
            Destroy(gameObject, 2f);
        }
    }
}
