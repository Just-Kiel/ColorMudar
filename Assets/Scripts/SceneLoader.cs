using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public GameObject loadingMenu; //objet contenant les éléments de l'écran de chargement
    [SerializeField] public Text loadingText; //texte de progressin du chargement
    [SerializeField] public GameObject currentObject; //objet du menu courant

    //coroutine de chargement
    public IEnumerator LoadLevel(string nameScene)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(nameScene); //chargement asynchrone de la scene suivante

        currentObject.SetActive(false);
        loadingMenu.SetActive(true); //on affiche l'écran de chargement

        //tant que la scene suivante n'est pas chargée
        while (!loading.isDone)
        {
            float progressLoad = Mathf.Clamp01(loading.progress / 0.9f);

            loadingText.text = progressLoad * 100 + "%"; //le texte de progression s'incrémente

            yield return null;
        }
    }
}
