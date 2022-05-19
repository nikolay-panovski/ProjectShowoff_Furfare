using UnityEngine;

public enum ProjectileState
{
    IDLE,   // does nothing - when on the ground and yet to be picked up
    HELD,
    FIRED
}

/* Projectile object (currently instantiated on pre-determined Spawnpoints).
 * Can be held and fired (caused by Player), bounce (physics), can hit another player (destruction event).
 * Needs to tell a Spawnpoint when it gets picked up and vacates the spot.
 */
public class Projectile : Item
{
    [SerializeField] protected int _speed;
    [SerializeField] protected int _maxBounces = 3;
    private int _bounceCount;   // to only see in inspector, don't serialize, go to the vertical ... near the padlock > choose Debug view

    public ProjectileState state { get; set; } = ProjectileState.IDLE;

    private Rigidbody myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();

        //eventQueue = FindObjectOfType<EventQueue>();
    }

    private void Update()
    {
        if (state == ProjectileState.HELD)
        {
            setPositionRelativeToHoldingPlayer();
        }
    }

    /// <summary>
    /// Fires the current projectile off of a player. Base guarantees the traversal of a single projectile.
    /// Specify extra functionality like more bullets in overrides.
    /// </summary>
    /// <param name="direction"></param>
    public virtual void Fire(Vector3 direction)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), owningPlayer.GetComponent<Collider>());
        state = ProjectileState.FIRED;
        SetForceInDirection(direction);
    }

    public void SetForceInDirection(Vector3 newDirection)
    {
        //myRigidbody.AddForce(newDirection * _speed, ForceMode.VelocityChange);  // to Impulse if balls will have a difference by Mass
        myRigidbody.velocity = newDirection * _speed;
    }

    private void checkForMaxBounceCount()
    {
        if (_bounceCount >= _maxBounces) onMaxBounceCount();
    }

    private void incrementBounceCount()
    {
        _bounceCount += 1;
    }

    private void setPositionRelativeToHoldingPlayer()
    {
        Transform player = owningPlayer.transform;

        transform.position = new Vector3(player.position.x + player.forward.x * player.localScale.x,
                                         transform.position.y,
                                         player.position.z + player.forward.z * player.localScale.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == ProjectileState.FIRED)
        {
            incrementBounceCount();
            checkForMaxBounceCount();
        }
    }

    protected virtual void onMaxBounceCount()
    {
        Destroy(gameObject);
    }
}
