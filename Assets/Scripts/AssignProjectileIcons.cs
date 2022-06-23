using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignProjectileIcons : MonoBehaviour
{
    [SerializeField] private Image _mySprite;
    private PlayerConfig _myPlayer;

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (_myPlayer.player.heldProjectile != null) _mySprite.sprite = _myPlayer.player.heldProjectile._displaySprite;
        else _mySprite.sprite = null;
    }

    public void SetConnectedPlayer(PlayerConfig pPlayer)
    {
        _myPlayer = pPlayer;
    }
}
