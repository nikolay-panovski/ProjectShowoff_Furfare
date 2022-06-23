using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CharacterSelection;
    public GameObject Settings;
    public GameObject Infographic;
    public void CharacterSelectionOn()
    {
        ForwardButtonSound();
        GotoScene.OnSceneChangeButton("CharacterSelectVisual - Copy");
    }
    public void SettingsOn()
    {
        MainMenu.gameObject.SetActive(false);
        Settings.gameObject.SetActive(true);
    }
    public void Controls()
    {
        Settings.gameObject.SetActive(false);
        Infographic.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ForwardButtonSound()
    {
        SoundPlay.PlaySound(SoundPlay.Sound.selected_Button);
    }
    public void BackButtonSound()
    {
        SoundPlay.PlaySound(SoundPlay.Sound.cancel_Button);
    }
}
