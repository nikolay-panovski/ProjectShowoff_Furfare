using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsSwitch : MonoBehaviour
{
    /*GameObject[] Event;
    Rumble rmb;
    List<Player> playersList = new List<Player>();
    [SerializeField] private Sprite X2;
    [SerializeField] private Sprite Reflexes;
    private void Start()
    {
        GetAllPlayers();
        
        rmb = this.GetComponent<Rumble>();
        Event = GameObject.FindGameObjectsWithTag("Event");
    }
    private void GetAllPlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            playersList.Add(PlayerManager.Instance.GetPlayerAtIndex(i).player);
        }
        ChooseEvent(1);
    }
    public void ChooseEvent(int current)
    {
        
        switch (Random.Range(1, 4))
        {
            case 1:
                if (current == 1)
                {
                    Debug.Log("pwr is the same as before: " + current);
                    ChooseEvent(1);
                }
                else
                {
                    for (int j = 0; j < playersList.Count; j++)
                    {
                        Event[j].GetComponent<Image>().sprite = null;
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
                    Debug.Log("pwr is the same as before: " + current);
                    ChooseEvent(2);
                }
                else
                {
                    for (int j = 0; j < playersList.Count; j++)
                    {
                        Event[j].GetComponent<Image>().sprite = X2;
                    }
                    for (int i = 0; i < playersList.Count; i++)
                    {
                        playersList[i].amountX = 200;
                        Debug.Log("AmountX: " + playersList[i].amountX);
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
                    Debug.Log("pwr is the same as before: " + current);
                    ChooseEvent(3);
                }
                else
                {
                    for (int i = 0; i < playersList.Count; i++)
                    {
                        SimpleMoveController pl = playersList[i].GetComponent<SimpleMoveController>();
                        pl.speedX = 2;
                        Debug.Log("SpeedX: " + pl.speedX);
                        Debug.Log("Fast Reflexes Event On");
                    }
                    for (int j = 0; j < Event.Length; j++)
                    {
                        Event[j].GetComponent<Image>().sprite = Reflexes;
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
        for (int z = 0; z < playersList.Count; z++)
        {
            Player pl = playersList[z].GetComponent<Player>();
            pl.amountX = 100;
            Debug.Log("AmountX after wait: " + pl.amountX);
            Debug.Log("No Event Off");
        }
        ChooseEvent(2);
    }

    void ReflexesOff()
    {
        for (int z = 0; z < playersList.Count; z++)
        {
            SimpleMoveController pl = playersList[z].GetComponent<SimpleMoveController>();
            pl.speedX = 1;
            Debug.Log("SpeedX after wait: " + pl.speedX);
            Debug.Log("Fast Reflexes Event Off");
        }
        ChooseEvent(3);
    }*/
}
