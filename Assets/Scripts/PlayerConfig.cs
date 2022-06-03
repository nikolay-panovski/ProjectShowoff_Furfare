using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfig
{
    public PlayerInput input { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
    public Sprite characterSprite { get; set; }
}
