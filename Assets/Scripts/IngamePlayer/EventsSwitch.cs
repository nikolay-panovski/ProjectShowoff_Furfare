using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSwitch : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] x2;
    public GameObject[] reflexes;
    int pwr;
    AudioSource _as;
    public AudioClip event_On;
    private void Start()
    {
        ChooseEvent(1);
        _as = this.GetComponent<AudioSource>();
    }
    public void ChooseEvent(int current)
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                if (current == 1)
                {
                    Debug.Log("pwr is the same as before: " + pwr);
                    ChooseEvent(1);
                }
                else
                {
                    for (int j = 0; j < x2.Length; j++)
                    {
                        reflexes[j].gameObject.SetActive(false);
                        x2[j].gameObject.SetActive(false);
                    }
                    _as.PlayOneShot(event_On);
                    Debug.Log("No Event On");
                    Invoke("NoEventOff", 10f);
                }
                break;

            case 2:
                if (current == 2)
                {
                    Debug.Log("pwr is the same as before: " + pwr);
                    ChooseEvent(2);
                }
                else
                {
                    for (int j = 0; j < x2.Length; j++)
                    {
                        reflexes[j].gameObject.SetActive(false);
                        x2[j].gameObject.SetActive(true);
                    }
                    for (int i = 0; i < players.Length; i++)
                    {
                        Player pl = players[i].GetComponent<Player>();
                        pl.amountX = 200;
                        Debug.Log("AmountX: " + pl.amountX);
                        Debug.Log("2x Event On");
                    }
                    _as.PlayOneShot(event_On);
                    Invoke("X2Off", 10f);
                }
                break;
            case 3:
                if (current == 3)
                {
                    Debug.Log("pwr is the same as before: " + pwr);
                    ChooseEvent(3);
                }
                else
                {
                    for (int i = 0; i < reflexes.Length; i++)
                    {
                        SimpleMoveController pl = players[i].GetComponent<SimpleMoveController>();
                        pl.speedX = 2;
                        Debug.Log("SpeedX: " + pl.speedX);
                        Debug.Log("Fast Reflexes Event On");
                    }
                    for (int j = 0; j < x2.Length; j++)
                    {
                        reflexes[j].gameObject.SetActive(true);
                        x2[j].gameObject.SetActive(false);
                    }
                    _as.PlayOneShot(event_On);
                    Invoke("ReflexesOff", 10f);
                }
                break;
        }
    }
    void NoEventOff()
    {
        Debug.Log("No Event Off");
        ChooseEvent(1);
    }

    void X2Off()
    {
        for (int z = 0; z < players.Length; z++)
        {
            Player pl = players[z].GetComponent<Player>();
            pl.amountX = 100;
            Debug.Log("AmountX after wait: " + pl.amountX);
            Debug.Log("No Event Off");
        }
        ChooseEvent(2);
    }

    void ReflexesOff()
    {
        for (int z = 0; z < players.Length; z++)
        {
            SimpleMoveController pl = players[z].GetComponent<SimpleMoveController>();
            pl.speedX = 1;
            Debug.Log("SpeedX after wait: " + pl.speedX);
            Debug.Log("Fast Reflexes Event Off");
        }
        ChooseEvent(3);
    }
}
