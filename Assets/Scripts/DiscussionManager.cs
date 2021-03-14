using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionManager : MonoBehaviour
{

    public GameObject DiscussionBox;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetButtonDown("Horizontal"))
        {
            DiscussionBox.SetActive(false);
        }
    }
}
