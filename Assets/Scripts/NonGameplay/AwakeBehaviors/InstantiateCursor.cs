using UnityEngine;
using UnityEngine.UI;

/* Instantiate a visual Cursor GameObject as a child to an InputObject Prefab used for a controller.
 * TODO: Communicate with GameManager on whether there is already a cursor, possibly whether this script should be attached or deleted.
 */
public class InstantiateCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorPrefab;
    [SerializeField] private Sprite cursorImage;

    // https://answers.unity.com/questions/889689/spriterenderer-on-top-of-canvases.html
    private void Awake()
    {
        //GameObject cursor = new GameObject("Cursor", typeof(SpriteRenderer), typeof(ControllerCursor));
        //cursor.transform.parent = this.transform;
        GameObject cursor = Instantiate(cursorPrefab, this.transform);

        if (cursorImage == null) Debug.LogError("Cursor image not assigned! Player input is in, but cursor will be invisible!");
        else
        {
            cursor.GetComponent<SpriteRenderer>().sprite = cursorImage;
        }
    }
}
