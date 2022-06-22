using UnityEngine;
using UnityEngine.InputSystem;

/* Based on PlayerSpawnpoint.
 */
public class InGameCardSpawner : MonoBehaviour
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
            //spawnPlayerCardTiedToPlayer() - start with spawning by playerID (try do score sorting later):
            PlayerConfig player = PlayerManager.Instance.GetPlayerAtIndex(playerIDToSpawn - 1);

            // find the InGameUI prefab object (containing the same-named script), pass it for player cards to be attached as direct children:
            //Transform UIObjectParent = FindObjectOfType<InGameUI>().gameObject.transform;

            GameObject functionalPlayerObject = Instantiate(
                PlayerManager.Instance.GetCardSpriteAtIndex(player.characterIndex), this.transform);
            //functionalPlayerObject.transform.position = this.transform.position;

            //eventQueue.AddEvent(new PlayerSpawnedInGameplayEventData(player));
        }
    }
}
