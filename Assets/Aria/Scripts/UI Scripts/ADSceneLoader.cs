using UnityEngine;
using UnityEngine.SceneManagement;

public class ADSceneLoader : MonoBehaviour
{
    private void Start()
    {
        
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
