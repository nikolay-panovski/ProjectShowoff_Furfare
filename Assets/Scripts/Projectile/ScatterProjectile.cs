using UnityEngine;

public class ScatterProjectile : Projectile
{
    [SerializeField] private int numOfProjectiles;
    [Tooltip("The angle by which each projectile after the first one will be offset from the player forward direction.")]
    [SerializeField] private float scatterAngle;

    public override void Fire(Vector3 direction)
    {
        for (int i = 0; i < numOfProjectiles; i++)
        {
            // rotate each bullet after the first one, based on the first one
            // (currently left hand-biased, can refine based on numOfProjectiles % 2 if needed)
            Vector3 offsetDirection = setScatterOffset(direction, i);

            // instantiate the extra projectiles from here
            Projectile scatterProjectile = Instantiate<Projectile>(this, transform.position + offsetDirection, Quaternion.identity);

            // base.Fire():
            //scatterProjectile.Fire(offsetDirection);  // this shorthand would work if the owningPlayer was ever set there
            Physics.IgnoreCollision(scatterProjectile.GetComponent<Collider>(), owningPlayer.GetComponent<Collider>());
            scatterProjectile.state = ProjectileState.FIRED;
            scatterProjectile.SetVelocityInDirection(offsetDirection);
        }

        // destroy self, it did its job
        Destroy(gameObject);
    }

    private Vector3 setScatterOffset(Vector3 initialDirection, int angleOffsetSteps)
    {
        Quaternion rotationOffset;

        if (angleOffsetSteps % 2 == 0) // 1st projectile is rotated 0 steps * scatterAngle, subsequent odds are rotated in +Y direction
        {
            rotationOffset = Quaternion.Euler(0f, scatterAngle * (angleOffsetSteps - 1), 0f);
        }
        else // even projectiles are rotated in -Y direction
        {
            rotationOffset = Quaternion.Euler(0f, -scatterAngle * (angleOffsetSteps - 1), 0f);
        }

        return rotationOffset * initialDirection;
    }
}
