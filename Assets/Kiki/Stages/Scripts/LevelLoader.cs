using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitonTime = 1f;

    // Update is called once per frame
    void Update()
    {
        //Press the key
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play anmation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(levelIndex);

        //Load
        SceneManager.LoadScene(levelIndex);
    }

}
