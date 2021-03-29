using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject newItem; //setup du prefab

    [SerializeField] private Vector3 center; //centre de la zone d'apparition
    [SerializeField] private Vector3 size; //dimensions de la zone d'apparition
    [SerializeField] private float minDelay; //délai minimum d'apparition
    [SerializeField] private float maxDelay; //délai maximum d'apparition

    // Start is called before the first frame update
    void Start()
    {
        SpawnItem(); //appel de la fonction
    }

    //gere la position d'apparition des items et le timing entre 2 apparitions
    void SpawnItem()
    {
        float randomDelay = Random.Range(minDelay, maxDelay); //délai d'apparition de l'item randomisé
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); //position d'apparition de l'item randomisé
        Instantiate(newItem, pos, Quaternion.identity); //création d'une nouvelle instance d'item avec position random

        Invoke("SpawnItem", randomDelay); //apparition d'un item avec un délai random
    }

    //visualisation de la zone d'apparition des items
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
