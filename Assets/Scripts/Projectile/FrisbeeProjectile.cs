using UnityEngine;

public class FrisbeeProjectile : Projectile
{
    [SerializeField] private int maxReturns;
    private int timesReturned = 0;

    [Tooltip("How much faster or slower relative to the initial speed the projectile will return to the player.")]
    [SerializeField] private float returnSpeedMultiplier = 1f;

    // TODO: investigate/replace with event communication
    protected override void Awake()
    {
        base.Awake();
        holdAfterFire = true;
    }

    protected override void Update()
    {
        base.Update();

        if (state == ProjectileState.RETURNING)
        {
            Vector3 distToPlayer = getDistToPlayer();
            SetVelocityInDirection(distToPlayer.normalized * returnSpeedMultiplier);

            if (distToPlayer.magnitude <= owningPlayer.transform.localScale.magnitude) regrabOnReturnCollision();
        }
    }

    protected override void onMaxBounceCount()
    {
        // ~~dirty. part of telling the player whether to hold onto the projectile or not. we want to free it on the last fire.
        if (timesReturned == maxReturns - 1) beforeLastFire();

        if (timesReturned < maxReturns)
        {
            // ~~technical consideration: this collides with players on the return if they are in the way, but does NOT destroy self or them
            gameObject.layer = LayerMask.NameToLayer("Ignore Walls");
            state = ProjectileState.RETURNING;
            _bounceCount = 0;
            returnToPlayer();
        }

        else Destroy(gameObject);
    }

    private void regrabOnReturnCollision()
    {
        if (getDistToPlayer().magnitude <= owningPlayer.transform.localScale.magnitude)
        {
            state = ProjectileState.HELD;
            gameObject.layer = LayerMask.NameToLayer("Projectiles");
        }
    }

    private void returnToPlayer()
    {
        Vector3 distToPlayer = getDistToPlayer();
        SetVelocityInDirection(distToPlayer * returnSpeedMultiplier);
        timesReturned++;
    }

    private Vector3 getDistToPlayer()
    {
        return owningPlayer.transform.position - this.transform.position;
    }

    private void beforeLastFire()
    {
        holdAfterFire = false;
    }
}
