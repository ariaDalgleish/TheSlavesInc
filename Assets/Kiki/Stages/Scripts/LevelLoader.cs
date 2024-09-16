using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;  // Reference to the Animator controlling transition animations
    public float transitionTime = 1f;  // Time duration for the transition animation

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            // Check for continuous press in the FixedUpdate
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LoadStageTwo();
            }
        }
    }




    public void LoadScoreScene()
    {
        // Load the "ScoreScene" after Stage1 (you can adjust the delay if needed)
        StartCoroutine(LoadLevelWithDelay("ScoreScene", 2f));  // Replace with the actual name of your score scene
    }

    public void LoadStageTwo()
    {
        // Start loading the "Stage2" scene immediately
        StartCoroutine(LoadLevelWithDelay("Stage2", 0f));  // Replace with the actual name of Stage 2
    }

    // Coroutine to load a scene after a delay, using scene name
    public IEnumerator LoadLevelWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay before proceeding

        // Directly load the scene without transition
        SceneManager.LoadScene(sceneName);
    }


    
    
}
