using UnityEngine;

/* Spawnpoint is a dedicated position which can spawn a pickup on itself. It communicates its existence to a manager
 * (currently: PickupSpawner), its spawning action, and takes communication about a pickup being picked up to "free" itself for a new spawn.
 */
public class Spawnpoint : MonoBehaviour
{
    [SerializeField] Item[] _pickupPrefab;

    private EventQueue eventQueue;

    public bool hasSpawnedPickup { get; private set; }

    private void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();
        eventQueue.Subscribe(EventType.PICKUP_PICKED, OnPickupPicked);
        eventQueue.AddEvent(new SpawnpointInittedEventData(this));
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PICKUP_PICKED, OnPickupPicked);
    }

    public void SpawnPickup()
    {
        int randomNumber = Random.Range(0, _pickupPrefab.Length);
        Item spawnedPickup = Instantiate(_pickupPrefab[randomNumber], transform.position, Quaternion.identity);
        spawnedPickup.originalSpawnpoint = this;

        eventQueue.AddEvent(new PickupSpawnedEventData(spawnedPickup, this));   // pickup currently does NOT consume this so the oSp is to be set above
        hasSpawnedPickup = true;
    }

    private void OnPickupPicked(EventData eventData)
    {
        SoundPlay.PlaySound(SoundPlay.Sound.projectile);
        PickupPickedEventData data = (PickupPickedEventData)eventData;
        if (data.originalSpawnpoint == this)
        {
            hasSpawnedPickup = false;
        }
    }
}
