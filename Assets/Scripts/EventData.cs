public enum EventType
{
    SPAWNPOINT_INITTED,
    PICKUP_SPAWNED,
    PICKUP_PICKED,
    PROJECTILE_FIRED,
    //PROJECTILE_DESTROYED,
    PLAYER_HIT
}

public class EventData
{
    public readonly EventType eventType;

    public EventData(EventType type)
    {
        eventType = type;
    }
}

public class SpawnpointInittedEventData : EventData
{
    public readonly Spawnpoint spawnpoint;
    public SpawnpointInittedEventData(Spawnpoint pSpawnpoint) : base(EventType.SPAWNPOINT_INITTED)
    {
        spawnpoint = pSpawnpoint;
    }
}

public class PickupSpawnedEventData : EventData
{
    // TODO: Projectile -> Item
    public readonly Projectile pickup;
    public PickupSpawnedEventData(Projectile pPickup) : base(EventType.PICKUP_SPAWNED)
    {
        pickup = pPickup;
    }
}

public class PickupPickedEventData : EventData
{
    // TODO: Projectile -> Item
    public readonly Projectile pickup;
    public readonly Spawnpoint originalSpawnpoint;
    public PickupPickedEventData(Projectile pPickup, Spawnpoint pOriginalSpawnpoint) : base(EventType.PICKUP_PICKED)
    {
        pickup = pPickup;
        originalSpawnpoint = pOriginalSpawnpoint;
    }
}

public class ProjectileFiredEventData : EventData
{
    public readonly Player playerWhoFired;  

    public ProjectileFiredEventData(Player pPlayer) : base(EventType.PROJECTILE_FIRED)
    {
        playerWhoFired = pPlayer;
    }
}

public class PlayerHitEventData : EventData
{
    public readonly Player hitPlayer;
    public readonly Player byPlayer;

    public PlayerHitEventData(Player pPlayer, Player pByPlayer) : base(EventType.PLAYER_HIT)
    {
        hitPlayer = pPlayer;
        byPlayer = pByPlayer;
    }
}