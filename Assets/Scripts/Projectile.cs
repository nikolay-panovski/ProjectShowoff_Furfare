using System.Collections;
using System.Collections.Generic;
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
    private int _bounceCount;

    private ProjectileState state = ProjectileState.IDLE;

    private GameObject holdingPlayer = null;    // TODO: player-specific class, NOT GameObject

    private Rigidbody myRigidbody;

    private Spawnpoint _originalSpawnpoint;

    public void SetDirection(Vector3 newDirection)
    {
        myRigidbody.AddForce(newDirection * Time.deltaTime * _speed);
        GetComponent<Collider>().isTrigger = false;     // else no collisions happen
    }

    public void SetState(ProjectileState newState)
    {
        state = newState;
    }

    public void SetHoldingPlayer(GameObject player)
    {
        holdingPlayer = player;
    }

    private void CheckCollisionCount()
    {
        if (_bounceCount >= _maxBounces) Destroy(gameObject);
    }

    private void setPositionRelativeToHoldingPlayer()
    {
        // basically assumes that bullets and player are of the same size
        // PLUS some extra value on the forward, otherwise an immediate collision with the player happens (+1 to bounce count)
        transform.position = holdingPlayer.transform.position + holdingPlayer.transform.forward * 1.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == ProjectileState.FIRED)
        {
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
}
