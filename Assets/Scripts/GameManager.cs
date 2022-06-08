using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Singleton game manager.
 * Rework the singleton part?
 * 
 * TODO? character select scene manager subscribes (listener) to the events sent by the player/cursor objects (the Move, Click etc.)
 * (figure out how it looks like)
 */
public class GameManager : MonoBehaviour
{
    private EventQueue eventQueue;

    // ~~GameManager (DontDestroyOnLoad) holds an array with sprites for the players to obtain after character selection and on gameplay screen
    // in reality, MIGHT consolidate with other properties in projected PlayerConfig struct
    private List<Sprite> cursorSprites = new List<Sprite>();

    private List<PlayerInput> joinedPlayerInputs = new List<PlayerInput>();
    //private List<PlayerConfig> players = new List<PlayerConfig>();
    private int joinedPlayers = 0;

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.CONTROLLER_JOINED, OnControllerJoined);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.CONTROLLER_JOINED, OnControllerJoined);
    }

    private void OnControllerJoined(EventData eventData)
    {
        ControllerJoinedEventData data = (ControllerJoinedEventData)eventData;
        joinedPlayerInputs.Add(data.playerInput);
        joinedPlayers++;
    }
}
