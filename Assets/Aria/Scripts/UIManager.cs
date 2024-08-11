using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject LevelFinishedScreen;

    private void Awake()
    {
        if(instance == null)
        {
            // If no instance exists, set this as the instance and don't destroy it on load
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // An instance already exists, destroy this object
            Destroy(gameObject);
        }

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
