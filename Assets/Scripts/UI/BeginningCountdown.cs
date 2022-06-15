using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginningCountdown : MonoBehaviour
{
    public Text countdowns;
    public int timerText;
    public GameObject[] players;
    public Spawnpoint[] spawns;
    public EventsSwitch eventsSwitch;
    public InGameUI ui;
    void Start()
    {
        StartCoroutine(Countdown(timerText));
    }
    void Update()
    {
        
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            countdowns.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
            Debug.Log("count " + count);
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
        for (int i = 0; i < players.Length; i++)
        {
            spawns[i].enabled = true;
        }
        ui.enabled = true;
        countdowns.enabled = false;
        eventsSwitch.enabled = true;
    }
}
