using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehavior : MonoBehaviour
{

    [SerializeField] private GameObject DiscussionBox;
    [SerializeField] private Rigidbody2D rb2DKing;

    [SerializeField] private int speedMove;

    [SerializeField] private Renderer oui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DiscussionBox.activeSelf == false)
        {
            //Debug.Log("jem le chokola");
            rb2DKing.velocity = Vector2.right * speedMove;
        }

        //Debug.Log(oui.isVisible);
    }

    void OnBecameInvisible()
    {
        Destroy(oui);
    }
}
