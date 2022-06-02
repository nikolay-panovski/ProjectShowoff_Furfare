using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private EventQueue eventQueue;

    private void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();
        eventQueue.Subscribe(EventType.PLAYER_HIT, OnPlayerHit);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_HIT, OnPlayerHit);
    }

    private void OnPlayerHit(EventData eventData)
    {
        PlayerHitEventData data = (PlayerHitEventData)eventData;
        IncreaseScore(data.byPlayer);
    }

    public void IncreaseScore(Player ofPlayer)
    {
        ofPlayer.UIText.text = ofPlayer.GetScore() + "Points: ";
    }
}
