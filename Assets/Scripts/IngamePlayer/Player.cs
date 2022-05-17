using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Player class to attach to player object. Holds references to relevant functionality components (hold, shoot etc.)
 * and to relevant objects to communicate with (held projectile, if any; powerups if any?; game manager for stats.)
 */
public class Player : MonoBehaviour
{
    //private GameManager gameManager;

    private PickupController catcher;   // ~~RequireComponent does not work, bless.
    private ShootController shooter;

    private Projectile heldProjectile = null;

    //private List<Powerup> powerups = new List<Powerup>();     // to game manager?

    [Tooltip("Time before and after collision with a fired projectile in which the player can pick it up instead of getting hurt.")]
    [SerializeField] private float bufferTime = 0.5f;
    private float timeBetweenCatchAndCollision = 0f;
    private bool isAttemptingCatch = false;

    void Start()
    {
        if (!TryGetComponent<PickupController>(out catcher)) Debug.LogError("Player is missing a PickupController-type script!");
        if (!TryGetComponent<ShootController>(out shooter)) Debug.LogError("Player is missing a ShootController-type script!");
    }

    void Update()
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
            heldProjectile = null;
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        Transform coll = collision.transform;
        Projectile incomingProjectile;

        if (coll.gameObject.TryGetComponent<Projectile>(out incomingProjectile))
        {
            // try handle projectile pickup on any player-projectile collision
            // (previously: picked static ones up from OnTriggerEnter)
            handleProjectileCatch(incomingProjectile);
        }

        //else literally any other collision possible (even with generic walls)
    }

    private void handleProjectileCatch(Projectile projectile)
    {
        ProjectileState projectileState = projectile.GetState();

        if (projectileState == ProjectileState.IDLE)
        {
            catcher.pickProjectileUp(projectile);
        }
        else if (projectileState == ProjectileState.FIRED)
        {
            if (readyToCatchBeforeCollision())
            {
                catcher.pickProjectileUp(projectile);
                heldProjectile = projectile;
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
            Utils.incrementTimer(timeBetweenCatchAndCollision);

            if (isAttemptingCatch == true)
            {
                catcher.pickProjectileUp(projectile);
                yield break;
            }
        }

        //_scoreManager.IncreaseScore(enemyPlayerNumber);   // submit signal to GameManager or a ScoreManager?
        takeDamage();
        Destroy(projectile.gameObject);
    }

    private void takeDamage()
    {
        Debug.Log("ouch!");
    }
}
