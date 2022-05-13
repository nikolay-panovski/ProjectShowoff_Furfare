using UnityEngine;

public enum ProjectileState
{
    IDLE,   // unused? - when on the ground and yet to be picked up
    HELD,
    FIRED
}

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed = 100;
    [SerializeField] private int _maxBounces = 3;
    [SerializeField] private int _bounceCount;

    private float _bufferAfterBounce = 0.5f;
    private bool _hitWallRecently = false;

    private ProjectileState state = ProjectileState.IDLE;

    private GameObject holdingPlayer = null;    // TODO: player-specific class, NOT GameObject

    private Rigidbody myRigidbody;

    private Spawnpoint _originalSpawnpoint;

    private int _originalShooter;

    #region GETTERS/SETTERS
    public ProjectileState GetState()
    {
        return state;
    }

    public GameObject GetHoldingPlayer()
    {
        return holdingPlayer;
    }

    public void SetState(ProjectileState newState)
    {
        state = newState;
    }

    public void SetHoldingPlayer(GameObject player)
    {
        holdingPlayer = player;
    }
    #endregion

    // ~~misleading method name
    public void SetDirection(Vector3 newDirection)
    {
        myRigidbody.AddForce(newDirection * Time.deltaTime * _speed);
        GetComponent<Collider>().isTrigger = false;     // else no collisions happen
    }

    private void CheckCollisionCount()
    {
        if (_bounceCount >= _maxBounces) Destroy(gameObject);
    }

    public void SetOriginalShooter(int number)
    {
        _originalShooter = number;
    }

    public int GetOriginalShooter()
    {
        return _originalShooter;
    }

    private void setPositionRelativeToHoldingPlayer()
    {
        Transform player = holdingPlayer.transform;

        transform.position = new Vector3(player.position.x + player.forward.x * player.localScale.x,
                                         transform.position.y,
                                         player.position.z + player.forward.z * player.localScale.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == ProjectileState.FIRED)
        {
            toggleHitWallState();   // true
            Invoke("toggleHitWallState", _bufferAfterBounce);   // false

            _bounceCount += 1;
            CheckCollisionCount();

            SetHoldingPlayer(null);

            if (_originalSpawnpoint != null)_originalSpawnpoint.SetHasSpawnedPickup(false);
        }
    }

    public void SetOriginalSpawnpoint(Spawnpoint myspawnpoint)
    {
        _originalSpawnpoint = myspawnpoint;
    }

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (state == ProjectileState.HELD)
        {
            setPositionRelativeToHoldingPlayer();
        }
    }

    private void toggleHitWallState()
    {
        _hitWallRecently ^= true;
    }
}
