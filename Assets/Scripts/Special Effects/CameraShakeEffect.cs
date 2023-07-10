using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CameraShakeEffect : MonoBehaviour
{
    [Tooltip("Adjust the shake strength in the direction between the two colliding player positions with this.")]
    [SerializeField] private float velocityMultiplier = 1f;

    private CinemachineImpulseSource source;

    private EventQueue eventQueue;

    void Start()
    {

        if (!TryGetComponent<CinemachineImpulseSource>(out source)) { Debug.LogWarning("[CameraShakeEffect] No CinemachineImpulseSource found. Add one or use another shake method!"); }

        eventQueue = FindObjectOfType<EventQueue>();
        eventQueue.Subscribe(EventType.PLAYER_HIT, GenerateImpactImpulse);
    }

    private void GenerateImpactImpulse(EventData eventData)
    {
        PlayerHitEventData data = (PlayerHitEventData)eventData;

        source.GenerateImpulseAt(data.hitPlayer.transform.position,
            (data.hitPlayer.transform.position - data.byPlayer.transform.position).normalized * velocityMultiplier);
        // would implement a stronger shake based on difference in scores, but here once again we have a Player/PlayerConfig data separation problem
    }

    void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_HIT, GenerateImpactImpulse);
    }
}
