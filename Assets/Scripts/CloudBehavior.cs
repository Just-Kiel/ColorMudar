using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2DCloud;
    [SerializeField] private float speedMove;

    // Update is called once per frame
    void Update()
    {
        rb2DCloud.velocity = Vector2.right * speedMove;
    }
}
