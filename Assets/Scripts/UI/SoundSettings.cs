using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    bool MusicIsOn = true;
    bool SoundIsOn = true;
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
        if (MusicIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
        }
        if (MusicIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
        }
    }

    public void MusicToggle()
    {
        if (MusicIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
            MusicIsOn = false;
        }
        if (MusicIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
            MusicIsOn = true;
        }
    }
    public void SoundToggle()
    {
        if (SoundIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
            SoundIsOn = false;
        }
        if (SoundIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
            SoundIsOn = true;
        }
    }
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundSettings");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
