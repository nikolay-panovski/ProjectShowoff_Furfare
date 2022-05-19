using UnityEngine;
using UnityEngine.InputSystem;

public class InstantiateCursor : MonoBehaviour
{
    [SerializeField] private Sprite cursorImage;

    // https://answers.unity.com/questions/889689/spriterenderer-on-top-of-canvases.html
    public void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined");
        if (!TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))  // prevent multiple attachment on already set up players
        {
            SpriteRenderer cursorRenderer = player.gameObject.AddComponent<SpriteRenderer>();
            cursorRenderer.sprite = cursorImage;
        }
    }
}
