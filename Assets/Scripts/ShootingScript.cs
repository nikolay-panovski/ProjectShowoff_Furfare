using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    private Projectile heldProjectile;
    [SerializeField] private int _cooldownDuration = 1;
    private bool _canFire = false;      // currently equivalent to "is holding a projectile" (source irrelevant)

    [Tooltip("Time before and after collision with a fired projectile in which the player can pick it up instead of getting hurt.")]
    [SerializeField] private float bufferTime = 0.5f;
    private float timeBetweenGrabAndCollision = 0f;
    private bool isAttemptingGrab = false;

    private void SetCanFire(bool trueOrFalse)
    {
        _canFire = trueOrFalse;
    }

    private void Fire()
    {
        //Set own forward vector for now, replace with vector from the controller joystick
        heldProjectile.SetState(ProjectileState.FIRED);
        heldProjectile.SetDirection(transform.forward * 1.2f);
        SetCanFire(false);
        heldProjectile = null;
    }

    public void OnGrab(InputValue value)
    {
        if (isAttemptingGrab == false) isAttemptingGrab = true;
        Debug.Log("Grab attempted.");
    }

    public void OnFire(InputValue value)
    {
        if (_canFire) Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectilePickup") && _canFire == false)
        {
            pickProjectileUp(other.gameObject.GetComponent<Projectile>());     // dirty, should not be here
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform coll = collision.transform;

        if (coll.CompareTag("ProjectilePickup"))
        {
            Projectile incomingProjectile = coll.gameObject.GetComponent<Projectile>();     // can fail if no Projectile script attached
            if (isAttemptingGrab)   // can validly grab - the button was pressed up to bufferTime ago
            {
                pickProjectileUp(incomingProjectile);
            }
            else // is NOT attempting grab - await to be pressed up to bufferTime late
            {
                StartCoroutine(delayCollisionDamage(incomingProjectile));
            }
        }
    }

    private void Update()
    {
        if (isAttemptingGrab == true)
        {
            incrementTimer();
            checkTimer();
        }
    }

    private void incrementTimer()
    {
        timeBetweenGrabAndCollision += Time.deltaTime;
    }

    private bool checkTimer()
    {
        bool hasTimerElapsed = timeBetweenGrabAndCollision >= bufferTime;

        if (hasTimerElapsed)
        {
            turnOffGrabState();
        }

        return hasTimerElapsed;
    }

    private void turnOffGrabState()
    {
        timeBetweenGrabAndCollision = 0.0f;
        isAttemptingGrab = false;
    }

    private IEnumerator delayCollisionDamage(Projectile projectile)
    {
        while (!checkTimer())
        {
            incrementTimer();

            if (isAttemptingGrab == true)
            {
                pickProjectileUp(projectile);
                yield break;
            }
        }

        //takeDamage();     // or addScore() or whatever equivalent fits
        Debug.Log("ouch!");
        Destroy(projectile.gameObject);    // dirty, I guess?
                                           // Destroy(projectile) will only destroy the script and keep the ball alive forever! :)
    }

    private void pickProjectileUp(Projectile projectile)
    {
        heldProjectile = projectile;
        heldProjectile.SetHoldingPlayer(this.gameObject);
        heldProjectile.SetState(ProjectileState.HELD);

        _canFire = true;
        Debug.Log("Grab successful.");
    }
}
