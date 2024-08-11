using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject LevelFinishedScreen;

    private void Awake()
    {
        LevelFinishedScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        


    }

   
    public void LevelFinished() // Method that is called when the timer reaches 0
    {
        Debug.Log("Timer reached 0! Method in another script was called.");
        LevelFinishedScreen.SetActive(true);
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetUI()
    {
        LevelFinishedScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
