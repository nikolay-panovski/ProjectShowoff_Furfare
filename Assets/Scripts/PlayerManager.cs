using System.Collections.Generic;
using UnityEngine;

/* Singleton player manager - for decoupling different player-related objects and keeping persistent information about players (inputs).
 * Rework the singleton part?
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
    public int numJoinedPlayers { get { return joinedPlayers.Count; } }

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
                    // ~~do not reset player index for now
                }
                else
                {
                    player.characterModel = data.selectedCharacter;
                    player.isReady = true;
                    player.characterIndex = data.selectedCharacterID;
                }
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
        if (index < 0 || index >= cursorSprites.cursorSprites.Count) return null;
        else return cursorSprites.cursorSprites[index];
    }

    public GameObject GetCharacterAtIndex(int index) // NULLABLE
    {
        if (index < 0 || index >= characterModels.characterModels.Count) return null;
        else return characterModels.characterModels[index];
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
