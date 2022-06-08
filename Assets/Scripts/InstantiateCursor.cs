using UnityEngine;

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
