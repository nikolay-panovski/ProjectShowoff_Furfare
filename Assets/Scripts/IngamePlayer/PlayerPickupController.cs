using UnityEngine;

public class PlayerPickupController : PickupController
{
    public override bool pickProjectileUp(Projectile projectile)
    {
        if (projectile.GetHoldingPlayer() == null)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), projectile.GetComponent<Collider>());
            projectile.SetHoldingPlayer(this.gameObject);
            projectile.SetState(ProjectileState.HELD);

            Debug.Log("Catch successful.");

            return true;
        }

        else Debug.Log("Pickup failure. Are you trying to catch a projectile held by another player?");
        return false;
    }
}
