using UnityEngine;

public class PlayerPickupController : PickupController
{
    public override void PickProjectileUp(Projectile projectile)
    {
        //Physics.IgnoreCollision(this.GetComponent<Collider>(), projectile.GetComponent<Collider>());  // unnecessary here, projectile handles it
        projectile.owningPlayer = this.gameObject.GetComponent<Player>();
        projectile.state = ProjectileState.HELD;

        //Debug.Log("Catch successful.");
    }
}
