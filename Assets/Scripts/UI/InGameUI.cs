using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    //Timer
    public int _timeRemaining;
    public bool timeIsLeft;
    public Text timeText;

    //Places
    private List<PlayerConfig> players = new List<PlayerConfig>();
    private EventQueue eventQueue;

    [SerializeField] private Text[] _allScoreText;

    [SerializeField] Sprite[] Sprites;
    [SerializeField] Image[] PlayerPlaces;

    //Infographic
    public GameObject InGameUIScreen;
    public GameObject Countdown;
    // Start is called before the first frame update
    void Start()
    {

        GetAllPlayers();

        timeIsLeft = true;

        eventQueue = FindObjectOfType<EventQueue>();
        eventQueue.Subscribe(EventType.PLAYER_HIT, OnPlayerHit);
        Invoke("DecreaseTimer", 1f);
    }

    private void GetAllPlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            players.Add(PlayerManager.Instance.GetPlayerAtIndex(i));
        }
    }

    public void StartCountDown()
    {
        InGameUIScreen.gameObject.SetActive(true);
        Countdown.gameObject.SetActive(true);
    }

    private void DecreaseTimer()
    {
        _timeRemaining -= 1;
        DisplayTime(_timeRemaining);
        CheckForMatchEnd();
        if (_timeRemaining > 0) Invoke("DecreaseTimer", 1f);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CheckForMatchEnd()
    {
        if (_timeRemaining <= 0) DetermineWinner();
    }

    private void OnPlayerHit(EventData eventData)
    {
        PlayerHitEventData data = (PlayerHitEventData)eventData;
        IncreaseScore(data.byPlayer, 100);
        SortPlayerList();
    }

    public void IncreaseScore(Player playerID, int amount)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (playerID == players[i].gameplayInput.GetComponent<Player>())
            {
                players[i].score += amount;
            }
        }  
    }

    private void SortPlayerList()
    {
        for (int i = 0; i < players.Count - 1; i++)
        {
            PlayerConfig player0 = players[i];
            PlayerConfig player1 = players[i + 1];
            if (player0.score > player1.score) i = 0;
            players.Sort((player0, player1) => player0.score.CompareTo(player1.score));
        }

        players.Reverse();
        AssignPlacements();
    }

    private void AssignPlacements()
    {
        for(int i = 0; i < PlayerPlaces.Length; i++)
        {
            PlayerPlaces[i].sprite = Sprites[i];
        }
    }

    private void DetermineWinner()
    {
        SortPlayerList();
        Debug.Log("The winner is" + players[0]);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_HIT, OnPlayerHit);
    }
}
