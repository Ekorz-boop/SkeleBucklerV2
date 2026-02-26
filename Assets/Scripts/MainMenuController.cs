using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Make sure the string exactly matches your scene name!
    public string sceneToLoad = "MainVrScene";

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        // Note: This will not work in the Unity Editor, but it will work in a built application.
        // 
        Application.Quit();
    }
}