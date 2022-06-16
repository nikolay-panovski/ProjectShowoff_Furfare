using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResizeMode
{
    SPRITE,
    RECT_TRANSFORM
}

[RequireComponent(typeof(BoxCollider2D))]
/* For interactable 2D objects with any Sprites or RectTransforms on them,
 * this allows resizing the BoxCollider accordingly.
 */
public class ResizeBoxCollider2D : MonoBehaviour
{
    [SerializeField] private ResizeMode resizeMode = ResizeMode.SPRITE;

    private bool hasSprite = false;
    private bool hasRectTransform = false;

    private SpriteRenderer spriteImage = null;
    private RectTransform rectTransform = null;
    
    void Start()
    {
        hasRectTransform = TryGetComponent<RectTransform>(out rectTransform);
    }

    /* ~~kinda odd relationship that this and the UICursorSelector have
     */
    public void Resize()
    {
        // not in Start() because that is too early in the cursor instantiation pipeline specifically
        if (hasSprite == false) hasSprite = TryGetComponent<SpriteRenderer>(out spriteImage);

        switch (resizeMode)
        {
            case ResizeMode.SPRITE:
                if (hasSprite)
                {
                    resize(spriteImage.sprite.bounds.size);
                }
                else
                {
                    Debug.LogWarning("Trying to resize using SPRITE, but no Sprite found, make sure to assign one!");
                }
                break;
            case ResizeMode.RECT_TRANSFORM:
                if (hasRectTransform)
                {
                    resize(rectTransform.rect.size);
                }
                else
                {
                    Debug.LogWarning("Trying to resize using RECT_TRANSFORM, but no RectTransform found, make sure you are not using a regular Transform and did not intend to use a Sprite!");
                }
                break;
            default:
                throw new System.Exception("Assign valid Resize Mode in the Inspector for this script (and your colliders) to work!");
        }
    }

    /* Resize a box collider to the size of the specified object (currently Sprite or RectTransform).
     * 
     * extra TODO: add parameters to add offset/scale instead of taking 1:1 resize
     */
    private void resize(Vector3 toSizeOfObject)
    {
        GetComponent<BoxCollider2D>().size = toSizeOfObject;
    }
}
