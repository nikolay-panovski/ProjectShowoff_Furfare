using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private int _cooldownDuration = 1;
    [SerializeField] private Score _scoreManager;
    [SerializeField] private int _playerNumber;
    private Projectile heldProjectile;
    private bool _canFire = false;      // currently equivalent to "is holding a projectile" (source irrelevant)

    [Tooltip("Time before and after collision with a fired projectile in which the player can pick it up instead of getting hurt.")]
    [SerializeField] private float bufferTime = 0.5f;
    private float timeBetweenCatchAndCollision = 0f;
    private bool isAttemptingCatch = false;

    private void SetCanFire(bool trueOrFalse)
    {
        _canFire = trueOrFalse;
    }

    private void Fire()
    {
        heldProjectile.SetState(ProjectileState.FIRED);
        heldProjectile.SetDirection(transform.forward);
        SetCanFire(false);
        heldProjectile.SetOriginalShooter(_playerNumber);
        heldProjectile = null;
    }

    public void OnCatch(InputValue value)
    {
        if (isAttemptingCatch == false) isAttemptingCatch = true;
        Debug.Log("Catch attempted.");
    }

    public void OnFire(InputValue value)
    {
        if (_canFire) Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectilePickup") && _canFire == false)
        {
            pickProjectileUp(other.gameObject.GetComponent<Projectile>());     // dirty, to rework
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform coll = collision.transform;

        if (coll.CompareTag("ProjectilePickup"))
        {
            Projectile incomingProjectile = coll.gameObject.GetComponent<Projectile>();     // can fail if no Projectile script attached
            // can validly Catch - the button was pressed up to bufferTime ago
            if (incomingProjectile.GetState() == ProjectileState.FIRED && isAttemptingCatch)
            {
                pickProjectileUp(incomingProjectile);
            }
            else // is NOT attempting Catch - await to be pressed up to bufferTime late
            {
                StartCoroutine(delayCollisionDamage(incomingProjectile, incomingProjectile.GetOriginalShooter()));
            }
        }
    }

    private void Update()
    {
        if (isAttemptingCatch == true)
        {
            incrementTimer();
            checkTimer();
        }
    }

    private void incrementTimer()
    {
        timeBetweenCatchAndCollision += Time.deltaTime;
    }

    private bool checkTimer()
    {
        bool hasTimerElapsed = timeBetweenCatchAndCollision >= bufferTime;

        if (hasTimerElapsed)
        {
            turnOffCatchState();
        }

        return hasTimerElapsed;
    }

    private void turnOffCatchState()
    {
        timeBetweenCatchAndCollision = 0.0f;
        isAttemptingCatch = false;
    }

    // FOR NOW - ALSO talks to score manager to modify score. that should be in a different place.
    private IEnumerator delayCollisionDamage(Projectile projectile, int enemyPlayerNumber)
    {
        while (!checkTimer())
        {
            incrementTimer();

            if (isAttemptingCatch == true)
            {
                pickProjectileUp(projectile);
                yield break;
            }
        }

        //takeDamage();     // or addScore() or whatever equivalent fits
        _scoreManager.IncreaseScore(enemyPlayerNumber);
        Debug.Log("ouch!");
        Destroy(projectile.gameObject);    // dirty, I guess?
                                           // Destroy(projectile) will only destroy the script and keep the ball alive forever! :)
    }

    private void pickProjectileUp(Projectile projectile)
    {
        if (projectile.GetHoldingPlayer() == null)
        {
            heldProjectile = projectile;
            Physics.IgnoreCollision(this.GetComponent<Collider>(), heldProjectile.GetComponent<Collider>());
            heldProjectile.SetHoldingPlayer(this.gameObject);
            heldProjectile.SetState(ProjectileState.HELD);

            _canFire = true;
            Debug.Log("Catch successful.");
        }

        //else Debug.Log("Trying to catch projectile held by another player. Won't catch.");
    }
}
