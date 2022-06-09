using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/* Performs collider-based checks for overlaps between this item and the other one.
 * Currently the result is that if the first other item is strictly a Button, it should Select itself.
 * 
 * Would break/have to be extended if there are overlapping buttons.
 */
public class UISelectHandler : MonoBehaviour
{
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
        }
        else
        {
            Debug.LogWarning("Overlap doesn't have Button component. If you see this message, you should handle that.");
        }
    }
}
