using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : ShootController
{
    [SerializeField] private Animator _myAnimator;
    private bool _throwing = false;
    public override bool TryShoot(Projectile projectile)
    {
        if (projectile == null)
        {
            Debug.Log("Got no projectile to shoot.");
            return false;
        }

        else
        {
            // can add an Event, but for now... public Projectile Fire()
            projectile.Fire(transform.forward);
            ToggleShootAnimation();
            return true;
        }
    }

    private void ToggleShootAnimation()
    {
        _throwing = !_throwing;
        _myAnimator.SetBool("Throwing", _throwing);
        if (_throwing)Invoke("ToggleShootAnimation", 0.2f);
    }
}
