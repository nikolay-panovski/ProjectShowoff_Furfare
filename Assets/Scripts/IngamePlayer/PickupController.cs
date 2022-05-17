using UnityEngine;

/* Pickup controller is responsible for picking up and catching projectiles (static and moving), likely will have to abstract to any items.
 * It receives catch input, turns it into a query to make player ready to catch, and handles reference assignment when the action happens.
 * (What happens when it doesn't happen - damage? Handle in Player. Send a signal whether the pick/catch was successful.)
 */
public abstract class PickupController : MonoBehaviour
{
    // Projectile projectile -> Item item?
    // return bool -> Projectile? (or Item?)
    /// <summary> Attempts to pick a projectile up (and update the relevant references).
    /// TODO: Abstract for any kind of pickup?
    /// </summary>
    /// <returns> Whether the pickup was successful. </returns>
    public abstract bool pickProjectileUp(Projectile projectile);
}
