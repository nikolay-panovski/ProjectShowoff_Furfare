using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject SoundSwitchOn;
    public GameObject SoundSwitchOff;
    public GameObject MusicSwitchOn;
    public GameObject MusicSwitchOff;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void SettingsOn()
    {
        mainMenu.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
        if (SoundManager.MusicIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
        }
        if (SoundManager.MusicIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
        }
    }

    public void MusicToggle()
    {
        SoundManager.MusicIsOn = !SoundManager.MusicIsOn;

        if (SoundManager.MusicIsOn)
        {
            Debug.Log("MusicOn");
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
            SoundManager.MusicToggle();
            Debug.Log("MusicIsOn: " + SoundManager.SoundIsOn);
        }
        if (SoundManager.MusicIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
            Debug.Log("MusicOff");
            SoundManager.MusicToggle();
        }
    }
    public void SoundToggle()
    {
        if (SoundManager.SoundIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
            SoundManager.MusicToggle();
        }
        if (SoundManager.SoundIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
        }
        SoundManager.SoundIsOn = !SoundManager.SoundIsOn;
    }
    void Awake()
    {
        /**
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundSettings");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        /**/
    }
}