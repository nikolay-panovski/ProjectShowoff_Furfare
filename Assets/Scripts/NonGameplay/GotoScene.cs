using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public static void OnSceneChangeButton(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public static void OnQuitButton()
    {
        Application.Quit(0);
    }
}
