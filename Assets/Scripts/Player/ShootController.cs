using UnityEngine;

public abstract class ShootController : MonoBehaviour
{
    protected bool canShoot;    // might be unnecessary

    /// <summary></summary>
    /// <returns> True if a shot occurred, false if it didn't (currently: if there was no projectile to shoot). </returns>
    public abstract bool TryShoot(Projectile projectile);
}
