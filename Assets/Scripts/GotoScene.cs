using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public void OnSceneChangeButton(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
