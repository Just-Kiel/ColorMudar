using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu; //objet contenant le menu d'options
    [SerializeField] private GameObject currentMenu; //objet contenant le menu courant
    [SerializeField] private GameObject selectMenu; //objet contenant le menu de sélection du personnage

    [SerializeField] private GameObject optionFirstButton; //objet du premier bouton dispo des options
    [SerializeField] private GameObject currentFirstButton; //objet du premier bouton dispo du menu courant
    [SerializeField] private GameObject selectFirstButton; //objet du premier bouton dispo du menu de sélection du personnage

    [SerializeField] private SceneLoader sceneLoader; //appel du script d'écran de chargement

    public static string currentPlayer; //personnage sélectionné

    public bool Pausing = false; //mode pause (inactivé)

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && optionMenu == true) //si appui sur le bouton de retour en étant sur le menu options
        {
            optionMenu.SetActive(false); //on ferme le menu options
            currentMenu.SetActive(true); //on affiche le menu courant
            EventSystem.current.SetSelectedGameObject(null); //le bouton préselectionné est reset
            EventSystem.current.SetSelectedGameObject(currentFirstButton); //on le définit sur le bouton du menu courant
        }
        
        if (Input.GetButtonDown("Cancel") && selectMenu == true) //si appui sur bouton de retour en étant sur menu de sélection de personnage (même principe que pour le menu d'options)
        {
            selectMenu.SetActive(false);
            currentMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(currentFirstButton);
        }
        
        if (Input.GetButtonDown("Cancel")) //si appui sur le bouton retour
        {
            currentMenu.SetActive(true); //on affiche le menu courant
            Pausing = true; //le mode pause est activé
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(currentFirstButton);
        }
    }

    //fonction du bouton Commencer
    public void StartButton()
    {
        PlayerPrefs.SetInt("nextLevel", 0); //la variable du level à charger démarre à 0
        currentMenu.SetActive(false);
        selectMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectFirstButton);
    }

    //fonction du bouton de sélection du personnage
    public void SelectButton()
    {
        currentPlayer = gameObject.name; //personnage courant récupère le nom du gameobject
        Debug.Log(currentPlayer);
        StartCoroutine(sceneLoader.LoadLevel("Intro")); //lancement de coroutine avec écran de chargement
    }

    //fonction du bouton Reprendre
    public void ResumeButton()
    {
        currentMenu.SetActive(false);
        Pausing = false;
    }

    //fonction du bouton Retour au menu principal
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    //fonction du bouton Options
    public void OptionsButton()
    {
        currentMenu.SetActive(false);
        optionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
    }

    //fonction du bouton Crédits
    public void CreditsButton()
    {
        //game object des crédits ?
    }

    //fonction du bouton Quitter
    public void QuitButton()
    {
        Application.Quit();
    }
}
