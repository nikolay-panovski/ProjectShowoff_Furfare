using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Needs to:
 * - Be attached to a general gameplay UI/canvas object.
 * - Respectively have access to UI elements - (player/placement/powerups) images, (timer/score) text
 * (GetComponent(InChildren) + elements are children of UI prefab, and FindObjectOfType<>:
 * require some distinction because almost everything on a UI is image or text;
 * FindWithTag: is distinctive but dirty;
 * an extra custom script class attached to each player card: is also distinctive, can be just as dirty or better;
 * is better if the referencing is done via events).
 * - Receive event updates that make it necessary to change the score (PLAYER_HIT, but a new PROP_HIT or similar *may* be created).
 * - Change the score on the correct player's UI card.
 * - Change the sprites for 1st/2nd/3rd/4th place if necessary, according to the score change.
 * 
 * - Timer here? Or hook up with TimeChecker?
 */
public class GameplayUI : MonoBehaviour
{
    private EventQueue eventQueue;

    // private List<?>

    void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.PLAYER_HIT, onPlayerHit);
    }

    private void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PLAYER_HIT, onPlayerHit);
    }

    private void onPlayerHit(EventData eventData)
    {
        PlayerHitEventData data = (PlayerHitEventData)eventData;

        // increase score of data.byPlayer
        // maybe fire check for switching sprites of places around
    }
}
