using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MenuGameManager : MonoBehaviour
{
    public bool IsMenuOpened = false;
    public GameObject menuUI;
    public GameObject timer;
    public GameObject health;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsMenuOpened == false)
        {
            
            health.SetActive(false);
            menuUI.SetActive(true);
            AudioListener.pause = true;
            IsMenuOpened = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && IsMenuOpened == true)
        {
            
            health.SetActive(true);
            menuUI.SetActive(false);
            AudioListener.pause = false;
            IsMenuOpened = false;
        }
    }

    public void LeaveGame()
    {
        Debug.Log("GameLeft");
        Application.Quit();
    }
}
