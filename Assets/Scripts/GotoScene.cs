using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public void OnSceneChangeButton(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void OnQuitButton()
    {
        Application.Quit(0);
    }
}
