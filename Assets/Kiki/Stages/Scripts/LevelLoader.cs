using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;  // Adjust this to the length of your transition animation

    private void Update()
    {
        // Check if the current scene is the score scene
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            // Listen for the 'Q' key press to load Stage 2
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q key pressed. Loading Stage 2...");
                LoadStageTwo();
            }
        }
    }

    public void LoadScoreScene()
    {
        StartCoroutine(LoadLevelWithDelay(SceneManager.GetActiveScene().buildIndex + 1, 2f)); // Use the new overload for int index
    }

    private void LoadStageTwo()
    {
        StartCoroutine(LoadLevelWithDelay("Stage2", 0f)); // Replace "Stage2Scene" with your actual Stage 2 scene name
    }

    // Overload for loading by scene name
    private IEnumerator LoadLevelWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play animation
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        // Load the specified scene by name
        SceneManager.LoadScene(sceneName);
    }

    // Overload for loading by scene index
    private IEnumerator LoadLevelWithDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play animation
        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        // Load the specified scene by index
        SceneManager.LoadScene(sceneIndex);
    }
}
