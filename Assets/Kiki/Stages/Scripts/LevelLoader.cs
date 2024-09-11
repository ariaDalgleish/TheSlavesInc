using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;  // Reference to the Animator controlling transition animations
    public float transitionTime = 1f;  // Time duration for the transition animation

    public void Update()
    {
        // Check if the current scene is the "ScoreScene"
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            // If the player presses the 'Q' key, load Stage 2
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q key pressed. Loading Stage 2...");  // Log the keypress to the console
                LoadStageTwo();  // Call the method to load Stage 2
            }
        }
    }

    public void LoadScoreScene()
    {
        // Start loading the next scene in the build index with a 2-second delay
        StartCoroutine(LoadLevelWithDelay(SceneManager.GetActiveScene().buildIndex + 1, 2f));
    }

    public void LoadStageTwo()
    {
        // Start loading the "Stage2" scene immediately with no delay
        StartCoroutine(LoadLevelWithDelay("Stage2", 0f));  // Replace "Stage2" with your actual scene name
    }

    // Coroutine to load a scene after a delay, using scene name
    public IEnumerator LoadLevelWithDelay(string ScoreScene, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay before proceeding

        // If a transition Animator is assigned, trigger the "Start" animation
        if (transition != null)
        {
            transition.SetTrigger("Start");  // Play the transition animation
            yield return new WaitForSeconds(transitionTime);  // Wait for the duration of the animation
        }

        // Load the scene by its name
        SceneManager.LoadScene(ScoreScene);
    }

    // Coroutine to load a scene after a delay, using scene index
    public IEnumerator LoadLevelWithDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay before proceeding

        // If a transition Animator is assigned, trigger the "Start" animation
        if (transition != null)
        {
            transition.SetTrigger("Start");  // Play the transition animation
            yield return new WaitForSeconds(transitionTime);  // Wait for the duration of the animation
        }

        // Load the scene by its index in the build settings
        SceneManager.LoadScene(sceneIndex);
    }
}
