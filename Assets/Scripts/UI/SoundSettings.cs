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
        if (SoundPlay.MusicIsOn)
        {
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);
        }
        if (SoundPlay.MusicIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
        }
        if (SoundPlay.SoundIsOn)
        {
            SoundSwitchOn.gameObject.SetActive(true);
            SoundSwitchOff.gameObject.SetActive(false);
        }
        if (SoundPlay.SoundIsOn == false)
        {
            SoundSwitchOn.gameObject.SetActive(false);
            SoundSwitchOff.gameObject.SetActive(true);
        }
    }

    public void MusicToggle()
    {
        if (SoundPlay.MusicIsOn)
        {
            Debug.Log("MusicOn");
            MusicSwitchOn.gameObject.SetActive(true);
            MusicSwitchOff.gameObject.SetActive(false);  
        }
        if (SoundPlay.MusicIsOn == false)
        {
            MusicSwitchOn.gameObject.SetActive(false);
            MusicSwitchOff.gameObject.SetActive(true);
            Debug.Log("MusicOff");
        }
        SoundManager.MusicToggle();
        SoundPlay.MusicIsOn = !SoundPlay.MusicIsOn;
    }
    public void SoundToggle()
    {
        if (SoundPlay.SoundIsOn)
        {
            SoundSwitchOn.gameObject.SetActive(false);
            SoundSwitchOff.gameObject.SetActive(true);
        }
        if (SoundPlay.SoundIsOn == false)
        {
            SoundSwitchOn.gameObject.SetActive(true);
            SoundSwitchOff.gameObject.SetActive(false);
        }
        SoundPlay.SoundIsOn = !SoundPlay.SoundIsOn;
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
