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
    SoundManager SoundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
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
        if (SoundManager.SoundIsOn)
        {
            SoundSwitchOn.gameObject.SetActive(true);
            SoundSwitchOff.gameObject.SetActive(false);
        }
        if (SoundManager.SoundIsOn == false)
        {
            SoundSwitchOn.gameObject.SetActive(false);
            SoundSwitchOff.gameObject.SetActive(true);
        }
    }

    public void MusicToggle()
    {
        if (SoundManager.MusicIsOn == false)
        {
            Debug.Log("MusicOn");
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);  
        }
        if (SoundManager.MusicIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
            Debug.Log("MusicOff");
        }
        SoundManager.MusicToggle();
        SoundManager.MusicIsOn = !SoundManager.MusicIsOn;
    }
    public void SoundToggle()
    {
        if (SoundManager.SoundIsOn)
        {
            SoundSwitchOn.gameObject.SetActive(false);
            SoundSwitchOff.gameObject.SetActive(true);
        }
        if (SoundManager.SoundIsOn == false)
        {
            SoundSwitchOn.gameObject.SetActive(true);
            SoundSwitchOff.gameObject.SetActive(false);
        }
        SoundManager.SoundToggle();
        SoundManager.SoundIsOn = !SoundManager.SoundIsOn;
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
