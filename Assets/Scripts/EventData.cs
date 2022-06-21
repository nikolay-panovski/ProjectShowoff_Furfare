public enum EventType
{
    PLAYERS_ENTERING_GAMEPLAY,
    PLAYER_SPAWNED_IN_GAMEPLAY,

    SPAWNPOINT_INITTED,
    PICKUP_SPAWNED,
    PICKUP_PICKED,
    PROJECTILE_FIRED,
    //PROJECTILE_DESTROYED,
    PLAYER_HIT,

    // WARNING: The below should be split IN A CLEAR WAY from the above (gameplay) events. In a different queue.
    // That would be the ideal case scenario.

    CONTROLLER_JOINED,  // new PlayerInput
    PLAYER_REGISTERED,  // PlayerInput recognized, PlayerConfig created out of it, added to manager and index assigned
    BUTTON_PRESSED,

    CHARACTER_SELECTED  // specifically a character button pressed
}

public class EventData
{
    public readonly EventType eventType;

    public EventData(EventType type)
    {
        eventType = type;
    }
}

public class PlayersEnteringGameplayEventData : EventData
{
    public readonly int numOfEnteringPlayers;

    public PlayersEnteringGameplayEventData(int pNumEnteringPlayers) : base(EventType.PLAYERS_ENTERING_GAMEPLAY)
    {
        numOfEnteringPlayers = pNumEnteringPlayers;
    }
}

public class PlayerSpawnedInGameplayEventData : EventData
{
    public readonly PlayerConfig spawnedPlayer;

    public PlayerSpawnedInGameplayEventData(PlayerConfig pPlayer) :base(EventType.PLAYER_SPAWNED_IN_GAMEPLAY)
    {
        spawnedPlayer = pPlayer;
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

public class PlayerRegisteredEventData : EventData
{
    public readonly PlayerConfig player;

    public PlayerRegisteredEventData(PlayerConfig pPlayer) : base(EventType.PLAYER_REGISTERED)
    {
        player = pPlayer;
    }
}

public class ButtonPressedEventData : EventData
{
    public readonly UICursorSelector byCursor;
    public readonly UnityEngine.UI.Button pressedButton;

    public ButtonPressedEventData(UICursorSelector pByCursor, UnityEngine.UI.Button pPressedButton) : base(EventType.BUTTON_PRESSED)
    {
        byCursor = pByCursor;
        pressedButton = pPressedButton;
    }
}

public class CharacterSelectedEventData : EventData
{
    public readonly UICursorSelector byCursor;
    public readonly UnityEngine.GameObject selectedCharacter;
    public readonly int selectedCharacterID;

    public CharacterSelectedEventData(UICursorSelector pByCursor, UnityEngine.GameObject pSelectedCharacter, int pID) : base(EventType.CHARACTER_SELECTED)
    {
        byCursor = pByCursor;
        selectedCharacter = pSelectedCharacter;
        selectedCharacterID = pID;
    }
}