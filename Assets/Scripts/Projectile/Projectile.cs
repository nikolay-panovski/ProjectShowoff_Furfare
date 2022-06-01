using UnityEngine;

public enum ProjectileState
{
    IDLE,   // does nothing - when on the ground and yet to be picked up
    HELD,
    FIRED,
    RETURNING   // returning to player after max bounce count
}

/* Projectile object (currently instantiated on pre-determined Spawnpoints).
 * Can be held and fired (caused by Player), bounce (physics), can hit another player (destruction event).
 * Needs to tell a Spawnpoint when it gets picked up and vacates the spot. (*this is now for Item)
 * Is designed to already work as a base, but some methods are virtual for altered implementations.
 */
public class Projectile : Item
{
    [SerializeField] protected int _speed;
    [SerializeField] protected int _maxBounces = 3;
    [SerializeField] ParticleSystem Impact = null;
    protected int _bounceCount;   // to only see in inspector, don't serialize, go to the vertical ... near the padlock > choose Debug view

    public ProjectileState state { get; set; } = ProjectileState.IDLE;

    protected bool holdAfterFire = false;

    private Rigidbody myRigidbody;

    protected virtual void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();

        //eventQueue = FindObjectOfType<EventQueue>();
    }

    protected virtual void Update()
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
        SetVelocityInDirection(direction);
    }

    public void SetVelocityInDirection(Vector3 newDirection)
    {
        // to Impulse if balls will have a difference by Mass... will it work?
        //myRigidbody.AddForce(newDirection * _speed, ForceMode.VelocityChange);
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
            if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                Instantiate(Impact, transform.position, Quaternion.identity);
                Debug.Log("particle");
            }
            
            incrementBounceCount();
            checkForMaxBounceCount();
        }
    }

    protected virtual void onMaxBounceCount()
    {
        Destroy(gameObject);
    }



    public bool GetHoldAfterFire()
    {
        return holdAfterFire;
    }
}
