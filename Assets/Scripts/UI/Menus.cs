using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CharacterSelection;
    public GameObject Infographic;
    public GameObject Settings;
    public void CharacterSelectionOn()
    {
        CharacterSelection.gameObject.SetActive(true);
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
}
