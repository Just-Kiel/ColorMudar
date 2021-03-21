using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
    //objets de tous les calques
    [SerializeField] private GameObject firstLayer;
    [SerializeField] private GameObject secondLayer;
    [SerializeField] private GameObject thirdLayer;
    [SerializeField] private GameObject fourthLayer;

    //vitesses de chaque calque à bouger
    [SerializeField] private float firstLayerSpeed;
    [SerializeField] private float secondLayerSpeed;
    [SerializeField] private float thirdLayerSpeed;
    [SerializeField] private float fourthLayerSpeed;

    //vecteurs de déplacement de chaque calque
    private Vector3 firstLayerOriginalPos;
    private Vector3 secondLayerOriginalPos;
    private Vector3 thirdLayerOriginalPos;
    private Vector3 fourthLayerOriginalPos;

    //décalage entre la position de départ et actuelle du joueur
    [SerializeField] private float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        //initialisation des positions de tous les calques
        firstLayerOriginalPos = firstLayer.transform.localPosition;
        secondLayerOriginalPos = secondLayer.transform.localPosition;
        thirdLayerOriginalPos = thirdLayer.transform.localPosition;
        fourthLayerOriginalPos = fourthLayer.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //mouvement de chaque calque en fonction de la vitesse respective et du delta entre départ et position actuelle du joueur
        firstLayer.transform.localPosition = firstLayerOriginalPos + Vector3.left * delta * firstLayerSpeed;
        secondLayer.transform.localPosition = secondLayerOriginalPos + Vector3.left * delta * secondLayerSpeed;
        thirdLayer.transform.localPosition = thirdLayerOriginalPos + Vector3.left * delta * thirdLayerSpeed;
        fourthLayer.transform.localPosition = fourthLayerOriginalPos + Vector3.left * delta * fourthLayerSpeed;
    }

    //fonction de déplacement de la parallaxe
    public void UpdateParallax(float playerDelta)
    {
        delta += playerDelta;
    }
}
