using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AraAra : MonoBehaviour
{
    public void SoundPlay()
    {
        AudioSource source = this.GetComponent<AudioSource>();
        source.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuVisual");
    }
}
