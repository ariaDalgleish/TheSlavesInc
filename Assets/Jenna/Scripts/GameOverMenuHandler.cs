using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuHandler : MonoBehaviour
{
    public void OnMenuButtonClicked()
    {
        Debug.Log("Menu button clicked! Attempting to load Menu scene.");
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Game quit");
        Application.Quit(); // This will quit the game
    }
}
