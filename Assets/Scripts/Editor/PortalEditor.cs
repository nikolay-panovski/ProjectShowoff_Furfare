using UnityEditor;

[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor
{
    Portal refPortal = null;

    private void OnSceneGUI()
    {
        if (refPortal == null) refPortal = (target as Portal).GetLinkedPortal();
        else
        {
            Handles.DrawLine((target as Portal).transform.position, refPortal.transform.position, 4.0f);
        }
    }
}
