using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Player class to attach to player object. Holds references to relevant functionality components (hold, shoot etc.)
 * and to relevant objects to communicate with (held projectile, if any; powerups if any?; game manager for stats.)
 */
public class Player : MonoBehaviour
{
    private EventQueue eventQueue;

    private PickupController catcher;
    private ShootController shooter;

    private Projectile heldProjectile = null;

    //private List<Powerup> powerups = new List<Powerup>();     // to game manager?

    [Tooltip("Time before and after collision with a fired projectile in which the player can pick it up instead of getting hurt.")]
    [SerializeField] private float bufferTime = 0.5f;
    private float timeBetweenCatchAndCollision = 0f;
    private bool isAttemptingCatch = false;

    void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        if (!TryGetComponent<PickupController>(out catcher)) throw new MissingComponentException("Player is missing a PickupController-type script!");
        if (!TryGetComponent<ShootController>(out shooter)) throw new MissingComponentException("Player is missing a ShootController-type script!");
    }

    void OnDestroy()
    {
        
    }

    #region ON INPUT EVENTS
    // ~~not in this class?
    public void OnCatch(InputValue value)
    {
        if (isAttemptingCatch == false) isAttemptingCatch = true;
        Debug.Log("Catch attempted.");
    }

    public void OnFire(InputValue value)
    {
        if (shooter.TryShoot(heldProjectile) == true)
        {
            //eventQueue.AddEvent(new ProjectileFiredEventData(this));
            heldProjectile = null;
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Treat collision with all relevant entities (right now: pickups)
        Projectile incomingProjectile;

        if (collision.gameObject.TryGetComponent<Projectile>(out incomingProjectile))
        {
            if (heldProjectile == null)
            {
                // try handle projectile pickup on any player-projectile collision, if player doesn't already have one
                // (previously: picked static ones up from OnTriggerEnter)

                // TODO: Consider returning to triggers for idle projectiles? Current alternative is layers magic.

                // ~~design choice: should player be able to catch projectiles if it is already holding one? (currently: no)
                // (if yes, send null check to pickProjectileUp and if it fails, keep the original heldProjectile reference)
                handleProjectileCatch(incomingProjectile);
            }
        }

        //else literally any other collision possible (even with generic walls)
    }

    private void handleProjectileCatch(Projectile projectile)
    {
        if (projectile.state == ProjectileState.IDLE)
        {
            pickProjectileUp(projectile);
        }
        else if (projectile.state == ProjectileState.FIRED)
        {
            if (readyToCatchBeforeCollision())
            {
                pickProjectileUp(projectile);
            }
            else // is NOT attempting Catch - await to be pressed up to bufferTime late, if no press then finally take damage
            {
                StartCoroutine(delayCollisionDamage(projectile));
            }
        }
    }

    private bool readyToCatchBeforeCollision()
    {
        return isAttemptingCatch;
    }

    private IEnumerator delayCollisionDamage(Projectile projectile)
    {
        while (!Utils.checkTimer(timeBetweenCatchAndCollision, bufferTime))
        {
            Utils.incrementTimer(ref timeBetweenCatchAndCollision);

            if (isAttemptingCatch == true)
            {
                pickProjectileUp(projectile);
                yield break;
            }
            //else yield return new WaitForEndOfFrame();
        }

        //_scoreManager.IncreaseScore(enemyPlayerNumber);   // submit signal to GameManager or a ScoreManager?
        eventQueue.AddEvent(new PlayerHitEventData(this, projectile.owningPlayer));
        takeDamage();
        Destroy(projectile.gameObject);
        Utils.resetTimer(ref timeBetweenCatchAndCollision);
    }

    private void takeDamage()
    {
        Debug.Log("ouch!");
    }

    private void pickProjectileUp(Projectile projectile)
    {
        catcher.PickProjectileUp(projectile);
        heldProjectile = projectile;
        eventQueue.AddEvent(new PickupPickedEventData(projectile, projectile.originalSpawnpoint));
    }
}
