using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FioleBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D fiole;

    [SerializeField] private DiscussionManager discuss;
    [SerializeField] private PlayerBehavior player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.animator.SetBool("isRunning", false);
            player.horizontalMove = 0;
            Debug.Log("bam on a la couleur");
            discuss.DiscussionBox.SetActive(true);
        }
    }
}
