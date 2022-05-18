using UnityEngine;

public enum ProjectileState
{
    IDLE,   // does nothing - when on the ground and yet to be picked up
    HELD,
    FIRED
}

/* Projectile object (currently instantiated on pre-determined Spawnpoints).
 * Can be held and fired (communication with Player), bounce (physics), can hit another player (destruction event).
 * Needs to tell a Spawnpoint when it gets picked up and vacates the spot.
 */
public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _maxBounces = 3;
    private int _bounceCount;   // to only see in inspector, don't serialize, go to the vertical ... near the padlock > choose Debug view

    private float _bufferAfterBounce = 0.5f;
    private bool _hitWallRecently = false;

    public ProjectileState state { get; set; } = ProjectileState.IDLE;

    public Player owningPlayer { get; set; } = null;

    private Rigidbody myRigidbody;

    public Spawnpoint originalSpawnpoint { get; set; }

    private void Start()
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

    public void SetForceInDirection(Vector3 newDirection)
    {
        myRigidbody.AddForce(newDirection * _speed, ForceMode.VelocityChange);  // to Impulse if balls will have a difference by Mass
    }

    private void CheckCollisionCount()
    {
        if (_bounceCount >= _maxBounces) Destroy(gameObject);
    }

    private void setPositionRelativeToHoldingPlayer()
    {
        Transform player = owningPlayer.transform;

        transform.position = new Vector3(player.position.x + player.forward.x * player.localScale.x,
                                         transform.position.y,
                                         player.position.z + player.forward.z * player.localScale.z);
    }

    // why is *almost any of this* in OnCollisionEnter?
    private void OnCollisionEnter(Collision collision)
    {
        /**
        if (state == ProjectileState.IDLE) return;
        // or something to that effect (disable collision of projectiles with projectiles, probably)
        /**/

        if (state == ProjectileState.FIRED)
        {
            toggleHitWallState();   // true
            Invoke("toggleHitWallState", _bufferAfterBounce);   // false

            _bounceCount += 1;
            CheckCollisionCount();
        }
    }

    private void toggleHitWallState()
    {
        _hitWallRecently ^= true;
    }
}
