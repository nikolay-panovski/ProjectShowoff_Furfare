using UnityEngine;

public class PlayerPickupController : PickupController
{
    public override bool PickProjectileUp(Projectile projectile)
    {
        if (projectile.owningPlayer != this) // re-check if this check is good (previous check was owningPlayer == null)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), projectile.GetComponent<Collider>());
            projectile.owningPlayer = this.gameObject.GetComponent<Player>();
            projectile.state = ProjectileState.HELD;

            Debug.Log("Catch successful.");

            return true;
        }

        //else Debug.Log("Pickup failure. Are you trying to catch a projectile held by another player?");
        return false;
    }
}
