using UnityEngine;

/* Spawnpoint is a dedicated position which can spawn a pickup on itself. It communicates its existence to a manager
 * (currently: PickupSpawner), its spawning action, and takes communication about a pickup being picked up to "free" itself for a new spawn.
 */
public class Spawnpoint : MonoBehaviour
{
    // TODO: Instantiate random from List of pickups instead? (where each instance of Projectile -> Item)
    // or, define spawnpoint types such as ProjectileSpawnpoint and PickupSpawnpoint?
    [SerializeField] Projectile _pickupPrefab;

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
        Projectile spawnedProjectile = Instantiate(_pickupPrefab, transform.position, Quaternion.identity);
        spawnedProjectile.originalSpawnpoint = this;

        eventQueue.AddEvent(new PickupSpawnedEventData(spawnedProjectile));
        hasSpawnedPickup = true;
    }

    private void OnPickupPicked(EventData eventData)
    {
        PickupPickedEventData data = (PickupPickedEventData)eventData;
        if (data.originalSpawnpoint == this)
        {
            hasSpawnedPickup = false;
        }
    }
}
