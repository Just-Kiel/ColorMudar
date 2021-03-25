using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2DFall;
    void Start()
    {
        rb2DFall = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Fall", 1.5f);
            Destroy(gameObject, 5f);
        }
    }

    void Fall()
    {
        rb2DFall.isKinematic = false;
    }
}
