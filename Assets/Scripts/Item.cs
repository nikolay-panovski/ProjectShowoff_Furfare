using UnityEngine;

/* An item which can be picked up from a Spawnpoint by a Player.
 */
public class Item : MonoBehaviour
{
    public Player owningPlayer { get; set; } = null;

    public Spawnpoint originalSpawnpoint { get; set; }
}
