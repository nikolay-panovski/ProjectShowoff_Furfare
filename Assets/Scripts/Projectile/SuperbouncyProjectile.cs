using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperbouncyProjectile : Projectile
{
    [SerializeField] float _speedIncrease = 0.2f;

    public override void SetVelocityInDirection(Vector3 newDirection)
    {
        myRigidbody.velocity = newDirection * _speed * ( 1 + (_bounceCount * _speedIncrease));
    }
}
