using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;

    // You can call this method from a button click or any other event
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}