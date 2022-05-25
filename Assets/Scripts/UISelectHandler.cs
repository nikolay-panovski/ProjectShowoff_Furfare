using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectHandler : MonoBehaviour
{
    private Camera mainCamera;
    private Ray cursorRay;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // raycast from camera through the position of the cursor (with what anchor - deal with that later)
        // cursor is in world space (as it is not attached to the canvas)
        cursorRay = getRayThroughObjectToCanvas(mainCamera.transform.position, this.transform.position);

        selectUIButtonViaRaycast();
    }

    private Ray getRayThroughObjectToCanvas(Vector3 fromCameraPosition, Vector3 throughObjectPosition)
    {
        // intended cursor point: top center (hardcoded)
        return new Ray(fromCameraPosition, new Vector3(throughObjectPosition.x - fromCameraPosition.x,
                                                       throughObjectPosition.y + this.transform.localScale.y * 0.5f - fromCameraPosition.y,
                                                       throughObjectPosition.z - fromCameraPosition.z));
    }

    private void selectUIButtonViaRaycast()
    {
        if (Physics.Raycast(cursorRay, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<Button>(out Button button))
            {
                // select button. a selected button reacts on clicks (or respective button press - Submit event trigger).
                button.Select();
            }
        }
        else EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(mainCamera.transform.position, this.transform.position);
    }
}
