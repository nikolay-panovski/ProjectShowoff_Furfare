using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsSwitch : MonoBehaviour
{
    GameObject[] players;
    GameObject[] events;
    [SerializeField] Sprite X2;
    [SerializeField] Sprite Reflexes;
    int pwr;
    Rumble rmb;
    private void Start()
    {
        rmb = this.GetComponent<Rumble>();
        Invoke("AssigningPlayersAndSprites", 5f);
    }
    public void AssigningPlayersAndSprites()
    {
        Debug.Log("AssigningPlayersAndSprites");
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players);
        events = GameObject.FindGameObjectsWithTag("Events");
        ChooseEvent(1);
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
                    for (int j = 0; j < events.Length; j++)
                    {
                        events[j].GetComponent<Image>().sprite = null;
                    }
                    SoundPlay.PlaySound(SoundPlay.Sound.eventActivates);
                    Debug.Log("No Event On");
                    Invoke("NoEventOff", 10f);
                    //rmb.RumbleConstant(1f, 1f, 1f);
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
                    for (int j = 0; j < events.Length; j++)
                    {
                        events[j].GetComponent<Image>().sprite = X2;
                    }
                    for (int i = 0; i < players.Length; i++)
                    {
                        InGameUI.ScoreX = 2;
                        Debug.Log("AmountX: " + InGameUI.ScoreX);
                        Debug.Log("2x Event On");
                    }
                    SoundPlay.PlaySound(SoundPlay.Sound.eventActivates);
                    Invoke("X2Off", 10f);
                    //rmb.RumbleConstant(1f, 1f, 1f);
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
                    for (int i = 0; i < players.Length; i++)
                    {
                        SimpleMoveController pl = players[i].GetComponent<SimpleMoveController>();
                        pl.speedX = 2;
                        Debug.Log("SpeedX: " + pl.speedX);
                        Debug.Log("Fast Reflexes Event On");
                    }
                    for (int j = 0; j < events.Length; j++)
                    {
                        events[j].GetComponent<Image>().sprite = Reflexes;
                    }
                    SoundPlay.PlaySound(SoundPlay.Sound.eventActivates);
                    Invoke("ReflexesOff", 10f);
                    //rmb.RumbleConstant(1f, 1f, 1f);
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
            InGameUI.ScoreX = 1;
            Debug.Log("AmountX after wait: " + InGameUI.ScoreX);
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
