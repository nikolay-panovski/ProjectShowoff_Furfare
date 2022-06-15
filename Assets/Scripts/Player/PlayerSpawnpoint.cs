using UnityEngine;
using UnityEngine.InputSystem;

/* At a minimum level, serves as a specific reference to an object at a certain position where a player should spawn.
 * 
 * Does NOT have a failsafe if the serialized field int is set incorrectly, especially for duplicate IDs.
 * Has a check whether it should spawn a player if not enough players have joined.
 */
public class PlayerSpawnpoint : MonoBehaviour
{
    private EventQueue eventQueue;

    [Tooltip("1-indexed player number to spawn at this location (if you want to spawn player 1 here, put 1, etc.)")]
    [SerializeField] private int playerIDToSpawn;

    private void Awake()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.PLAYERS_ENTERING_GAMEPLAY, onPlayersEnteredGameplay);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYERS_ENTERING_GAMEPLAY, onPlayersEnteredGameplay);
    }

    private void onPlayersEnteredGameplay(EventData eventData)
    {
        PlayersEnteringGameplayEventData data = (PlayersEnteringGameplayEventData)eventData;

        if (playerIDToSpawn - 1 < data.numOfEnteringPlayers)    // ~~or without -1, with <= ; weird 1-based indexing convention I went with
        {
            //spawnEnteredPlayerFunctionally():
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(playerIDToSpawn - 1);

            GameObject functionalPlayerObject = Instantiate(
                PlayerManager.Instance.GetCharacterAtIndex(player.characterIndex), player.gameObject.transform);
            functionalPlayerObject.transform.position = this.transform.position;

            //andAssignGameplayInputReference():
            player.gameplayInput = functionalPlayerObject.GetComponent<PlayerInput>();
            //player.gameplayInput.enabled = true;
        }
    }
}
