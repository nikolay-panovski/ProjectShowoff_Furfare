using Cinemachine;
using UnityEngine;

/* CinemachineTargetGroup requires a reference to the Transforms of the objects that are Targeted
 * by its effects (in our case: zooming in/out). This script provides them in line with the Event Queue processes.
 * 
 * One instance, attached to the object with the TargetGroup itself.
 */
public class AssignPlayersToCinemachine : MonoBehaviour
{
    private EventQueue eventQueue;

    private CinemachineTargetGroup targetGroup;

    [SerializeField] private float targetsWeight = 1;
    [SerializeField] private float targetsRadius = 6;

    private void Awake()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.PLAYER_SPAWNED_IN_GAMEPLAY, onPlayerSpawnedInGameplay);

        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_SPAWNED_IN_GAMEPLAY, onPlayerSpawnedInGameplay);
    }

    private void onPlayerSpawnedInGameplay(EventData eventData)
    {
        PlayerSpawnedInGameplayEventData data = (PlayerSpawnedInGameplayEventData)eventData;

        Transform functionalPlayerObjectTransform = data.spawnedPlayer.gameplayInput.transform;
        targetGroup.AddMember(functionalPlayerObjectTransform, targetsWeight, targetsRadius);
    }
}
