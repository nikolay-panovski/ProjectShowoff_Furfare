using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChecker : MonoBehaviour
{
    [SerializeField] private int _matchDuration = 120;
    [SerializeField] private Text _timeRemainingText;
    private int _timeRemaining;

    private void Start()
    {
        _timeRemaining = _matchDuration;
        Invoke("DecreaseTimer", 1f);
    }

    private void DecreaseTimer()
    {
        _timeRemaining -= 1;
        UpdateText();
        CheckForMatchEnd();
        if (_timeRemaining > 0) Invoke("DecreaseTimer", 1f);
    }

    private void UpdateText()
    {
        int minutesLeft = (int)Mathf.Floor(_timeRemaining / 60);
        int secondsLeft = _timeRemaining - (minutesLeft * 60);

        if (secondsLeft < 10) _timeRemainingText.text = minutesLeft + ":0" + secondsLeft;
        else _timeRemainingText.text = minutesLeft + ":" + secondsLeft;
    }

    private void CheckForMatchEnd()
    {
        if (_timeRemaining <= 0) DetermineWinner();
    }

    private void DetermineWinner()
    {
        Player[] allPlayers = FindObjectsOfType<Player>();
        Player playerWithHighestScore = null;
        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (playerWithHighestScore == null) playerWithHighestScore = allPlayers[i];
            //else if (allPlayers[i].GetScore() > playerWithHighestScore.GetScore()) playerWithHighestScore = allPlayers[i];
        }

        Debug.Log(playerWithHighestScore + " Is the winner");
    }
}
