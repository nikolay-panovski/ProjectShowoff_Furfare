using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/* Player class to attach to player object. Holds references to relevant functionality components (hold, shoot etc.)
 * and to relevant objects to communicate with (held projectile, if any; powerups if any?; game manager for stats.)
 */
public class Player : MonoBehaviour
{
    private EventQueue eventQueue;

    private PickupController catcher;
    private PlayerShootController shooter;
    private SimpleMoveController mover;
    private PlayerAnimator animator;
    private PlayerParticleController particles;

    private Projectile heldProjectile = null;

    // dirty connection to UI / for later on, the event-based problem is that a player needs a connection to a specific slider
    public UnityEngine.UI.Text UIText;
    
    [SerializeField] private float _stunDuration = 2;
    [SerializeField] private float _invincibilityDuration = 2;
    private int _score = 0;
    private bool stunned = false;
    private bool invincible = false;

    [Tooltip("Time before and after collision with a fired projectile in which the player can pick it up instead of getting hurt.")]
    [SerializeField] private float bufferTime = 0.5f;
    private float timeBetweenCatchAndCollision = 0f;
    private bool isAttemptingCatch = false;

    private Vector2 moveInput;  // store OnMove results here

    //UI Part
    public int PlayerID;
    public int amountX = 100;
    private Score _scoreManager;
    SoundManager sm;
    Rumble rmb;
    void Start()
    {
        eventQueue = FindObjectOfType<EventQueue>();

        if (!TryGetComponent<PickupController>(out catcher)) throw new MissingComponentException("Player is missing a PickupController-type script!");
        if (!TryGetComponent<PlayerShootController>(out shooter)) throw new MissingComponentException("Player is missing a ShootController-type script!");
        if (!TryGetComponent<SimpleMoveController>(out mover)) throw new MissingComponentException("Player is missing a SimpleMoveController-type script!");
        if (!TryGetComponent<PlayerAnimator>(out animator)) throw new MissingComponentException("Player is missing a PlayerAnimator-type script!");
        if (!TryGetComponent<PlayerParticleController>(out particles)) throw new MissingComponentException("Player is missing a ParticleController-type script!");

        //Sound Data
        sm = this.GetComponent<SoundManager>();
        rmb = this.GetComponent<Rumble>();
    }

    void OnDestroy()
    {
        
    }

    private void Update()
    {
        // revert stupidity if FrisbeeProjectile hits another player before its last return
        if (heldProjectile == null && gameObject.layer != LayerMask.NameToLayer("Players")) gameObject.layer = LayerMask.NameToLayer("Players");
    }

    private void FixedUpdate()
    {
        if (stunned == true)
        {
            mover.StopVelocity();
            return;
        }
        float movementSpeed = mover.Move(moveInput);
        if (movementSpeed > 0)
        {
            animator.SetFloat("Movement", movementSpeed);
            particles.PlayDustTrailAtPositionAndRotation(transform.position - transform.forward, transform.rotation);
        }
        else
        {
            animator.SetFloat("Movement", 0.0f);
            particles.StopDustTrail();
        }
    }

    #region ON INPUT EVENTS
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnCatch(InputValue value)
    {
        ToggleAttemptingCatch();
        Debug.Log("Catch attempted.");
    }

    public void OnFire(InputValue value)
    {
        if (stunned == true) return;
        if (shooter.TryShoot(heldProjectile) == true)
        {
            //eventQueue.AddEvent(new ProjectileFiredEventData(this));
            if (heldProjectile.GetHoldAfterFire() == false)  // keep "fired" state on player end if the projectile type demands it (FrisbeeProjectile)
            {
                heldProjectile = null;
                gameObject.layer = LayerMask.NameToLayer("Players");    // stop ignoring collisions with other projectiles after firing one
            }

            animator.SetBool("IsThrowing", true);
            //rmb.RumbleConstant(1f, 1f, 0.5f);
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (stunned == true || invincible == true) return;
        // TODO: Treat collision with all relevant entities (right now: pickups)
        Projectile incomingProjectile;

        if (collision.gameObject.TryGetComponent<Projectile>(out incomingProjectile))
        {
            handleProjectileCatch(incomingProjectile);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Portal collidedPortal;

        if (other.gameObject.TryGetComponent<Portal>(out collidedPortal))
        {
            if (collidedPortal.GetActiveStatus() == true)
            {
                TeleportToPortal(collidedPortal);
            }
        }
    }

    private void handleProjectileCatch(Projectile projectile)
    {
        if (stunned == true) return;
        if (projectile.state == ProjectileState.IDLE)   // is grabbing a pickup projectile that just floats in one place
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

    private void TeleportToPortal(Portal targetPortal)
    {
        //Cahnges the current position to the new position and resets the y axis
        Vector3 newposition = targetPortal.GetLinkedPortalPosition();
        newposition.y = transform.position.y;
        transform.position = newposition;
        //Turns both the collided portal off and the portal the player teleports to
        targetPortal.ToggleActive();
        targetPortal.GetLinkedPortal().ToggleActive();
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
       // projectile.owningPlayer.IncreaseScore(1);
        ToggleInvincibility();
        ToggleStun();
        particles.PlayStunParticle(transform.position + new Vector3 (0,4,0));
        takeDamage();
        particles.PlayImpactParticle(transform.position);
        animator.SetBool("IsStunned", true);
        Destroy(projectile.gameObject);
        Utils.resetTimer(ref timeBetweenCatchAndCollision);
    }

    private void pickProjectileUp(Projectile projectile)
    {
        catcher.PickProjectileUp(projectile);
        setOwnLayer("Ignore Pickup Projectiles");
        heldProjectile = projectile;
        eventQueue.AddEvent(new PickupPickedEventData(projectile, projectile.originalSpawnpoint));
    }

    private void setOwnLayer(string layer)
    {
        this.gameObject.layer = LayerMask.NameToLayer(layer);
    }

    public int GetScore()
    {
        return _score;
    }

    public void IncreaseScore(int enemyPlayerID, int amount)
    {
        int score = amount * amountX;

        if (_scoreManager == null) _scoreManager = FindObjectOfType<Score>();
        _scoreManager.IncreaseScore(enemyPlayerID, score);
    }

    public void ToggleAttemptingCatch()
    {
        // Consider yourself attempting catch, and exit this state after the defined catch duration
        isAttemptingCatch = !isAttemptingCatch;
        if (isAttemptingCatch == true) Invoke("ToggleAttemptingCatch", bufferTime);
    }

    public void ToggleStun()
    {
        //Turns stun on and after a delay turns it back off
        stunned = !stunned;
        if (!stunned) animator.SetBool("IsStunned", false);
        if (stunned) Invoke("ToggleStun", _stunDuration);
    }

    public void ToggleInvincibility()
    {
        //Turns invincibility on and after a delay turns it back off
        invincible = !invincible;
        if (invincible == true) Invoke("ToggleInvincibility", _invincibilityDuration);
    }
}