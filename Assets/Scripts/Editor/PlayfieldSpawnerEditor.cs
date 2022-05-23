using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayfieldSpawner))]
public class PlayfieldSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Playfield"))
        {
            PlayfieldSpawner spawner = target as PlayfieldSpawner;
            spawner.GeneratePlayfield();
        }
    }
}
