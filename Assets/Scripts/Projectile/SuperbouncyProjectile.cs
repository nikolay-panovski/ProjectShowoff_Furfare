using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperbouncyProjectile : Projectile
{
    [SerializeField] float _speedIncrease = 0.2f;

    public override void incrementBounceCount()
    {
        base.incrementBounceCount();
        myRigidbody.velocity = myRigidbody.velocity * (1 + _speedIncrease);
    }
}
