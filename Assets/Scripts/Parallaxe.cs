using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
    [SerializeField] private GameObject firstLayer;
    [SerializeField] private GameObject secondLayer;
    [SerializeField] private GameObject thirdLayer;
    [SerializeField] private GameObject fourthLayer;

    [SerializeField] private float firstLayerSpeed;
    [SerializeField] private float secondLayerSpeed;
    [SerializeField] private float thirdLayerSpeed;
    [SerializeField] private float fourthLayerSpeed;

    private Vector3 firstLayerOriginalPos;
    private Vector3 secondLayerOriginalPos;
    private Vector3 thirdLayerOriginalPos;
    private Vector3 fourthLayerOriginalPos;

    [SerializeField] private float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstLayerOriginalPos = firstLayer.transform.localPosition;
        secondLayerOriginalPos = secondLayer.transform.localPosition;
        thirdLayerOriginalPos = thirdLayer.transform.localPosition;
        fourthLayerOriginalPos = fourthLayer.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        firstLayer.transform.localPosition = firstLayerOriginalPos + Vector3.left * delta * firstLayerSpeed;
        secondLayer.transform.localPosition = secondLayerOriginalPos + Vector3.left * delta * secondLayerSpeed;
        thirdLayer.transform.localPosition = thirdLayerOriginalPos + Vector3.left * delta * thirdLayerSpeed;
        fourthLayer.transform.localPosition = fourthLayerOriginalPos + Vector3.left * delta * fourthLayerSpeed;
    }

    public void UpdateParallax(float playerDelta)
    {
        delta += playerDelta;
    }
}
