using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject optionFirstButton;
    public void StartButton()
    {
        SceneManager.LoadScene("MoveScene");
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
