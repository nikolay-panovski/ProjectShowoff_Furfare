using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProjectile : Projectile
{
    public override void Fire(Vector3 direction)
    {
        base.Fire(direction);
        setOwnLayer("Ignore Walls");  // this layer is now only for FIRED GHOST projectiles
    }
}
