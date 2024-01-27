using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;

    private void Update()
    {
        // Press any key to start!
        if (Input.anyKeyDown)
        {
            // Call your start game function or trigger an action here
            SceneManager.LoadScene(sceneName);
        }
    }

    public void EnterScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}