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
                if (player.characterModel != null)  // better: if (player.characterModel == data.selectedCharacter),
                                                    // but that would cause additional checks across this and CharacterDisplaySlot
                {
                    player.characterModel = null;
                    player.isReady = false;
                    player.characterIndex = data.selectedCharacterID;
                }
                else
                {
                    player.characterModel = data.selectedCharacter;
                    player.isReady = true;
                    // ~~do not reset player index for now
                }
            }
        }
    }

    /* NEXT FOCUS:
     * Create non-gameplay -> gameplay transition with focus on the managed player (input) objects.
     * In a clean(ish) way.
     */
    private void onSceneChanged(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        // happens on EACH new scene load, unless filtered:
        //print(loadedScene.name);
        if (loadedScene.name == "AssetsMaterialsLevel") // TODO: distinguish between gameplay and non-gameplay screen (bogus script?)
                                                        // for this check?
        {
            foreach (PlayerConfig player in joinedPlayers)
            {
                // all PlayerInput transforms currently at (0, 0, 0)
                GameObject functionalPlayerObject = Instantiate(characterModels.characterModels[player.characterIndex], player.gameObject.transform);
                // cursor object itself gets disabled, but not the parenting InputObject (and it shouldn't, only the relevant PlayerInput)
                player.cursorObject.gameObject.SetActive(false);
                // DIRTY - does not preserve original input (for exit out of level)
                player.input = functionalPlayerObject.GetComponent<PlayerInput>();
                // VERY DIRTY (obvious why). As mostly expected, does not prevent the bad from happening.
                FindObjectOfType<PlayerInputManager>().gameObject.SetActive(false);
            }
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
