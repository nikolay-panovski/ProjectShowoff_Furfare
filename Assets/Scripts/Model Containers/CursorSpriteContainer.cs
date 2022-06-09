using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CursorContainer", menuName = "Containers/Cursor Container")]
public class CursorSpriteContainer : ScriptableObject
{
    public List<Sprite> cursorSprites;
}