public enum EventType
{
    SPAWNPOINT_INITTED,
    PICKUP_SPAWNED,
    PICKUP_PICKED,
    PROJECTILE_FIRED,
    //PROJECTILE_DESTROYED,
    PLAYER_HIT,

    // WARNING: The below should be split IN A CLEAR WAY from the above (gameplay) events. In a different queue.
    // That would be the ideal case scenario.

    CONTROLLER_JOINED,
    CHARACTER_SELECTED
}

public class EventData
{
    public readonly EventType eventType;

    public EventData(EventType type)
    {
        eventType = type;
    }
}

#region GAMEPLAY EVENTS
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
    public readonly Item pickup;
    public readonly Spawnpoint atSpawnpoint;
    public PickupSpawnedEventData(Item pPickup, Spawnpoint pAtSpawnpoint) : base(EventType.PICKUP_SPAWNED)
    {
        pickup = pPickup;
        atSpawnpoint = pAtSpawnpoint;
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

#endregion

public class ControllerJoinedEventData : EventData
{
    public readonly UnityEngine.InputSystem.PlayerInput playerInput;

    public ControllerJoinedEventData(UnityEngine.InputSystem.PlayerInput pController) : base(EventType.CONTROLLER_JOINED)
    {
        playerInput = pController;
    }
}

public class CharacterSelectedEventData : EventData
{
    public readonly UnityEngine.GameObject chosenCharacter;

    public CharacterSelectedEventData(UnityEngine.GameObject pCharacterPrefab) : base(EventType.CHARACTER_SELECTED)
    {
        chosenCharacter = pCharacterPrefab;
    }
}