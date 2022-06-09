using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public GameObject CharacterSelection;
    public GameObject Infographic;
    public void CharacterSelectionOn()
    {
        CharacterSelection.gameObject.SetActive(true);
    }
    public void Controls()
    {
        Infographic.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
