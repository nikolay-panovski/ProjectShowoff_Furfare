using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Singleton player manager - for decoupling different player-related objects and keeping persistent information about players (inputs).
 * Rework the singleton part?
 * 
 * TODO? character select scene manager subscribes (listener) to the events sent by the player/cursor objects (the Move, Click etc.)
 * (figure out how it looks like)
 */
public class PlayerManager : MonoBehaviour
{
    private EventQueue eventQueue;

    [Tooltip("Set of cursor sprites to be used for the players (specify not only the sprites but also the order in the ScriptableObject itself).")]
    [SerializeField] private CursorSpriteContainer cursorSprites;
    [Tooltip("Set of character models to be used for the players (specify not only the models but also the order in the ScriptableObject itself).")]
    [SerializeField] private CharacterModelContainer characterModels;

    [SerializeField] private UICursorSelector cursorFunctionPrefab;

    private List<PlayerConfig> joinedPlayers = new List<PlayerConfig>();
    private int numJoinedPlayers { get { return joinedPlayers.Count; } }

    private static PlayerManager instance = null;

    public static PlayerManager Instance
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
            return;
        }
        else
        {
            instance = this;
        }

        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.CONTROLLER_JOINED, onControllerJoined);
        eventQueue.Subscribe(EventType.CHARACTER_SELECTED, onCharacterSelected);

        //SceneManager.activeSceneChanged += onSceneChanged;
        SceneManager.sceneLoaded += onSceneChanged;

        // DontDestroyOnLoad()s for important to preserve objects:
        // Player Input Manager, Event System
        // ~~SOMEONE's job, not necessarily of a PlayerManager

        // handle enabling and disabling of persistent objects when they should not operate on a scene (gameplay vs non-gameplay)
        DontDestroyOnLoad(FindObjectOfType<PlayerInputManager>().gameObject);
        if (UnityEngine.EventSystems.EventSystem.current != null)
            DontDestroyOnLoad(UnityEngine.EventSystems.EventSystem.current.gameObject);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.CONTROLLER_JOINED, onControllerJoined);
        eventQueue.Unsubscribe(EventType.CHARACTER_SELECTED, onCharacterSelected);
    }

    private void onControllerJoined(EventData eventData)
    {
        ControllerJoinedEventData data = (ControllerJoinedEventData)eventData;
        PlayerConfig joiningPlayer = new PlayerConfig(data.playerInput);

        updatePlayerListWithJoining(joiningPlayer);
        instantiateCursorForJoinedPlayer(joiningPlayer);
    }

    private void onCharacterSelected(EventData eventData)
    {
        CharacterSelectedEventData data = (CharacterSelectedEventData)eventData;

        foreach (PlayerConfig player in joinedPlayers)
        {
            if (data.byCursor == player.cursorObject)
            {
                if (player.characterModel == data.selectedCharacter)
                {
                    player.characterModel = null;
                    player.isReady = false;
                }
                else
                {
                    player.characterModel = data.selectedCharacter;
                    player.isReady = true;
                }
            }
        }
    }

    private void onSceneChanged(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        // on EACH new scene load, unless filtered:
        foreach (PlayerConfig player in joinedPlayers)
        {
            // EDIT: cursor is now persistent as a child of InputObject that is NOT on the canvas
            //instantiateCursorForJoinedPlayer(player);
        }
    }

    private void updatePlayerListWithJoining(PlayerConfig joiningPlayer)
    {
        joiningPlayer.playerIndex = numJoinedPlayers;   // assign index before adding self, because Lists are 0-indexed for 1st item
        joinedPlayers.Add(joiningPlayer);
    }

    private void instantiateCursorForJoinedPlayer(PlayerConfig joinedPlayer)
    {
        if (joinedPlayer.cursorObject != null)
        {
            throw new System.Exception("Player who just joined already has a cursor instantiated, this should not be the case! Trace back the code!");
        }

        UICursorSelector cursor = Instantiate(cursorFunctionPrefab, joinedPlayer.gameObject.transform);
        joinedPlayer.cursorObject = cursor;
        
        // send event with the index to allow the cursor to self-assign a sprite
        eventQueue.AddEvent(new PlayerRegisteredEventData(joinedPlayer.playerIndex));
    }

    // Assumed that controllers are not leaving in any way (there is no event for that and joinedPlayers will not be decremented).

    public PlayerConfig GetPlayerAtIndex(int index) // NULLABLE
    {
        if (index < 0 || index >= numJoinedPlayers) return null;
        else return joinedPlayers[index];
    }

    public Sprite GetSpriteAtIndex(int index)   // NULLABLE
    {
        if (index < 0 || index >= numJoinedPlayers) return null;
        else return cursorSprites.cursorSprites[index];
    }

    public bool GetAllPlayersReady()
    {
        bool allPlayersReady = true;

        foreach (PlayerConfig player in joinedPlayers)
        {
            if (player.isReady == false) allPlayersReady = false;
            break;
        }

        return allPlayersReady;
    }
}
