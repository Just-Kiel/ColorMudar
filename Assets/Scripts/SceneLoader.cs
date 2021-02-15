using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public GameObject loadingMenu;
    [SerializeField] public Text loadingText;
    [SerializeField] public GameObject currentObject;

    public IEnumerator LoadLevel(string nameScene)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(nameScene);

        currentObject.SetActive(false);
        loadingMenu.SetActive(true);

        while (!loading.isDone)
        {
            float progressLoad = Mathf.Clamp01(loading.progress / 0.9f);

            loadingText.text = progressLoad * 100 + "%";

            yield return null;
        }
    }
}
