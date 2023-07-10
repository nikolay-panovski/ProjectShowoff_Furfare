using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    [Tooltip("After the timer runs out for the current round, the game will wait for this many seconds before going to the next round.")]
    [SerializeField] private float idleTimeAfterRound = 3;
    private float _idleTimeAfterRound;
    //[SerializeField] private bool destroyObjsOnRoundEnd = true;
    
    //Timer
    public int _timeRemaining;
    public bool timeIsLeft;
    public Text timeText;

    public static int ScoreX = 1;
    //Places
    private List<PlayerConfig> players = new List<PlayerConfig>();
    private EventQueue eventQueue;

    [Tooltip("Sprites used for 1st/2nd/3rd/4th place. Please insert in that order.")]
    [SerializeField] Sprite[] RankingSprites;

    //Infographic (*unused?) public GameObject InGameUIScreen; public GameObject Countdown;

    private ScoreAnimationEffect scoreAnimationEffect;
    private TimeAnimationEffect timeAnimationEffect;
    private MusicTempoEffect musicTempoEffect;
    private HitStopEffect hitStopEffect;

    void Start()
    {

        GetAllPlayers();

        _idleTimeAfterRound = idleTimeAfterRound;

        timeIsLeft = true;
        timeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(_timeRemaining / 60), Mathf.FloorToInt(_timeRemaining % 60));

        scoreAnimationEffect = FindObjectOfType<ScoreAnimationEffect>();
        timeAnimationEffect = FindObjectOfType<TimeAnimationEffect>();
        musicTempoEffect = FindObjectOfType<MusicTempoEffect>();
        hitStopEffect = FindObjectOfType<HitStopEffect>();

        eventQueue = FindObjectOfType<EventQueue>();
        eventQueue.Subscribe(EventType.PLAYER_HIT, OnPlayerHit);
        Invoke("DecreaseTimer", 1f);

        foreach (PlayerConfig player in players)
        {
            player.playerUICard.GetComponentInChildren<Text>().text = player.score.ToString();
        }
    }

    private void GetAllPlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            players.Add(PlayerManager.Instance.GetPlayerAtIndex(i));
        }
    }

    private void DecreaseTimer()
    {
        _timeRemaining -= 1;
        DisplayTime(_timeRemaining);
        CheckForMatchEnd();
        if (_timeRemaining > 0) 
        {
            if (timeAnimationEffect != null)
            {
                timeAnimationEffect.SetTimerTextRef(timeText);  // generally bad idea if this happened every Update()
                timeAnimationEffect.CheckForBeforeRoundEnd(_timeRemaining);
            }
            if (musicTempoEffect != null)
            {
                if (_timeRemaining == musicTempoEffect.secondsBeforeRoundEnd)
                {
                    musicTempoEffect.SetMusicTempo();
                }
                else if (_timeRemaining == 1)
                {
                    musicTempoEffect.TurnMusicOff();
                }
            }
            Invoke("DecreaseTimer", 1f);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CheckForMatchEnd()
    {
        if (_timeRemaining <= 0)
        {
            if (_idleTimeAfterRound == idleTimeAfterRound)
            {
                if (hitStopEffect != null) hitStopEffect.SlowDownTime();
                // may also send signal that the round is ending, if it's needed

                /**
                if (destroyObjsOnRoundEnd)
                {
                    FindObjectOfType<PickupSpawner>().gameObject.SetActive(false);
                    foreach (Spawnpoint s in FindObjectsOfType<Spawnpoint>()) s.gameObject.SetActive(false);
                    foreach (Projectile p in FindObjectsOfType<Projectile>()) { if (p.owningPlayer == null) Destroy(p.gameObject); }
                }
                /**/
            }

            _idleTimeAfterRound -= Time.unscaledDeltaTime;
            if (_idleTimeAfterRound <= 0) GoToNextLevel();
            else CheckForMatchEnd();
        }
    }

    private void OnPlayerHit(EventData eventData)
    {
        PlayerHitEventData data = (PlayerHitEventData)eventData;
        IncreaseScore(data.byPlayer, 100* ScoreX);
        SortPlayerList();
    }

    public void IncreaseScore(Player playerID, int amount)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (playerID == players[i].player)
            {
                players[i].score += amount;

                if (scoreAnimationEffect != null)
                {
                    scoreAnimationEffect.AnimateScoreModification(players[i], players[i].score);
                }
                else
                {
                    players[i].playerUICard.GetComponentInChildren<Text>().text = players[i].score.ToString();  // no animation defined, so set score text directly
                }
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
        for(int i = 0; i < players.Count; i++)
        {
            players[i].playerUICard.GetComponentInChildren<PlayerRankPlace>().gameObject.GetComponent<Image>().sprite = RankingSprites[i];
        }
    }

    private void GoToNextLevel()
    {
        DestroyAllPlayerInstances();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void DestroyAllPlayerInstances()
    {
        List<PlayerConfig> allPlayers = new List<PlayerConfig>();

        for (int i = 0; i < PlayerManager.Instance.numJoinedPlayers; i++)
        {
            allPlayers.Add(PlayerManager.Instance.GetPlayerAtIndex(i));
        }

        for (int i = 0; i < allPlayers.Count; i++)
        {
            Destroy(allPlayers[i].player.gameObject);
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
