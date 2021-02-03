using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("MoveScene");
    }

    public void OptionsButton()
    {
        //mettre un game object qui apparait et disparait (comme ça je peux utiliser le meme script dans le menu pause)
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
