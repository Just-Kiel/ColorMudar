using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject optionFirstButton;
    [SerializeField] private GameObject currentFirstButton;

    public bool Pausing = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && optionMenu == true)
        {
            optionMenu.SetActive(false);
            currentMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(currentFirstButton);
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            currentMenu.SetActive(true);
            Pausing = true;
            //currentMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(currentFirstButton);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("MoveScene");
    }

    public void ResumeButton()
    {
        currentMenu.SetActive(false);
        Pausing = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OptionsButton()
    {
        currentMenu.SetActive(false);
        optionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
    }

    public void CreditsButton()
    {
        //game object des crédits ?
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
