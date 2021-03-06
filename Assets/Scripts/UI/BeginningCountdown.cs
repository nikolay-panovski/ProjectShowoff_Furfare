using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginningCountdown : MonoBehaviour
{
    public Text countdowns;
    public int timerText = 3;
    GameObject[] players;
    public EventsSwitch eventsSwitch;
    public InGameUI ui;
    void Start()
    {
        StartCoroutine(Countdown(timerText));
        Invoke("DuringCountdown", 1.0f);
    }
    void DuringCountdown()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            int countNew = count - 1;
            countdowns.text = countNew.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        StartGame();
    }
    void StartGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            Player playerScript = players[i].GetComponent<Player>();
            playerScript.enabled = true;
        }
        ui.enabled = true;
        countdowns.enabled = false;
        eventsSwitch.enabled = true;
    }
}
