using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/* Performs collider-based checks for overlaps between this item and the other one.
 * Currently the result is that if the other items is strictly a Button, it should Select itself.
 */
public class UISelectHandler : MonoBehaviour
{

    void Update()
    {
        /**
        // STOPPED:
        // raycast from camera through the position of the cursor (with what anchor - deal with that later)
        // cursor is in world space (as it is not attached to the canvas)
        cursorRay = getRayThroughObjectToCanvas(mainCamera.transform.position, this.transform.position);

        selectUIButtonViaRaycast();
        /**/

    }

    /**
    private Ray getRayThroughObjectToCanvas(Vector3 fromCameraPosition, Vector3 throughObjectPosition)
    {
        // intended cursor point: top center (hardcoded)
        return new Ray(fromCameraPosition, new Vector3(throughObjectPosition.x - fromCameraPosition.x,
                                                       throughObjectPosition.y + this.transform.localScale.y * 0.5f - fromCameraPosition.y,
                                                       throughObjectPosition.z - fromCameraPosition.z));
    }
    /**/

    /**
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
    /**/

    public Collider2D CheckForColliderOverlap(Collider2D thisCollider)
    {
        // prepare array to catch and return only 1st found overlap result - we have no reason to treat multiple
        Collider2D[] results = new Collider2D[1];
        thisCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);
        return results[0];
    }

    public void OnColliderOverlap(Collider2D overlap, out Button button)
    {
        if (overlap.TryGetComponent<Button>(out button))
        {
            button.Select();
            // TODO: deselect?
        }
        else
        {
            Debug.Log("No button overlap this frame. Last selected button should be null.");
        }
    }
}
