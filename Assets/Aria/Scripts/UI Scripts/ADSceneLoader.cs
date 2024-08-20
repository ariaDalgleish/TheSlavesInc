using UnityEngine;
using UnityEngine.SceneManagement;

public class ADSceneLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Enemy", LoadSceneMode.Additive);
        SceneManager.LoadScene("PuzzleScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
