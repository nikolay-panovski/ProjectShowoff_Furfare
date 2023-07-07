using UnityEngine;
using UnityEngine.UI;

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
    private EventQueue eventQueue;

    [SerializeField] protected int _speed;
    [SerializeField] protected int _maxBounces = 3;
    [SerializeField] protected float _bounceCooldown = 0.05f;
    [SerializeField] public Sprite _displaySprite;

    protected int _bounceCount;
    protected bool _justBounced = false;
    public ProjectileState state { get; set; } = ProjectileState.IDLE;

    protected bool holdAfterFire = false;

    protected Rigidbody myRigidbody;

    private ImpactParticleEffect impactParticleEffect;

    protected virtual void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        impactParticleEffect = FindObjectOfType<ImpactParticleEffect>();

        eventQueue = FindObjectOfType<EventQueue>();

        eventQueue.Subscribe(EventType.PICKUP_PICKED, onProjectilePicked);
    }

    protected void OnDestroy()
    {
        eventQueue.Unsubscribe(EventType.PICKUP_PICKED, onProjectilePicked);
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

        setOwnLayer("Fired Projectiles");
    }

    public virtual void SetVelocityInDirection(Vector3 newDirection)
    {
        myRigidbody.velocity = newDirection * _speed;
    }

    protected void checkForMaxBounceCount()
    {
        if (_bounceCount >= _maxBounces) onMaxBounceCount();
    }

    public virtual void incrementBounceCount()
    {
        if (_justBounced == false)
        {
            _bounceCount += 1;
            ToggleJustBounced();
        }
    }

    protected void setPositionRelativeToHoldingPlayer()
    {
        Transform player = owningPlayer.transform;

        transform.position = new Vector3(player.position.x + player.forward.x * player.localScale.x,
                                         transform.position.y,
                                         player.position.z + player.forward.z * player.localScale.z);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (state == ProjectileState.FIRED)
        {
            incrementBounceCount();
            checkForMaxBounceCount();
            if (impactParticleEffect != null) impactParticleEffect.PlayOnImpact(collision);
        }
    }

    protected virtual void onMaxBounceCount()
    {
        Destroy(gameObject);
    }

    protected void onProjectilePicked(EventData eventData)
    {
        PickupPickedEventData data = (PickupPickedEventData)eventData;
        if (data.pickup == this)
        {
            ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
            if (particles != null) Destroy(particles.gameObject);
        }
    }

    protected void setOwnLayer(string layer)
    {
        this.gameObject.layer = LayerMask.NameToLayer(layer);
    }

    protected void ToggleJustBounced()
    {
        //Turns justBounced on and after a delay turns it back off
        _justBounced = !_justBounced;
        if (_justBounced == true) Invoke("ToggleJustBounced", _bounceCooldown);
    }

    public bool GetHoldAfterFire()
    {
        return holdAfterFire;
    }
}
