using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussionManager : MonoBehaviour
{

    [SerializeField] private GameObject DiscussionBox;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            DiscussionBox.SetActive(false);
        }
    }
}
