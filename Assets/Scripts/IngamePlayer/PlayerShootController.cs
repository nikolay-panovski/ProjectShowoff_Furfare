using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : ShootController
{
    public override bool TryShoot(Projectile projectile)
    {
        if (projectile == null)
        {
            Debug.Log("Got no projectile to shoot.");
            return false;
        }

        else
        {
            projectile.SetState(ProjectileState.FIRED);
            projectile.SetHoldingPlayer(null);              // consider what this reference might be needed for (score?)
            projectile.SetDirection(transform.forward);     // the meat - the Rigidboy.AddForce() deal. change method name.

            return true;
        }
    }
}
