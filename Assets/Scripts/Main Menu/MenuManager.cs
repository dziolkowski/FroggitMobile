using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Funkcja wywolywana po kliknieciu "New Game"
    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Funkcja wywolywana po kliknieciu "Exit"
    public void ExitGame()
    {
        Application.Quit();
    }
}
