using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteContainer", menuName = "Containers/Sprite Container")]
public class SpriteContainer : ScriptableObject
{
    public List<Sprite> sprites;
}