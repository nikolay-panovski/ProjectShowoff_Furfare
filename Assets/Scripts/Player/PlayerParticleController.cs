using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    private EventQueue eventQueue;

    [SerializeField] private ParticleSystem dustTrailPrefab;
    [SerializeField] private ParticleSystem impactPrefab;
    [SerializeField] private ParticleSystem stunPrefab;

    private Player thisPlayer;  // ~~semi-dirty

    // Start is called before the first frame update
    void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.PLAYER_HIT, onPlayerHit);

        // find the Player component on the same hierarchy level (object) as this script
        thisPlayer = GetComponent<Player>();

        // ParticleSystem cannot work from a prefab reference, so instantiate from the prefab to have a scene
        // instance for it to work with:
        dustTrailPrefab = Instantiate(dustTrailPrefab);
        impactPrefab = Instantiate(impactPrefab);
        stunPrefab = Instantiate(stunPrefab);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_HIT, onPlayerHit);
    }

    private void onPlayerHit(EventData eventData)
    {
        PlayerHitEventData data = (PlayerHitEventData)eventData;

        if (data.hitPlayer == thisPlayer)
        {
            impactPrefab.Play();
            stunPrefab.Play();  // you're stunned at the same time as you're hit
        }
    }

    public void PlayDustTrail()
    {
        //dustTrailPrefab.Simulate(0.1f);
        dustTrailPrefab.Play();
    }

    public void PlayDustTrailAtPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        dustTrailPrefab.transform.SetPositionAndRotation(position, rotation);
        dustTrailPrefab.Play();
    }

    public void StopDustTrail()
    {
        dustTrailPrefab.Stop();
    }
}
