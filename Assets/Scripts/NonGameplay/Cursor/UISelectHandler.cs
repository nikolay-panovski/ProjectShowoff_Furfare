using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Performs collider-based checks for overlaps between this item and the other one.
 * Currently the result is that if the first other item is strictly a Button, it should Select itself.
 * 
 * Would break/have to be extended if there are overlapping buttons.
 */
public class UISelectHandler : MonoBehaviour
{
    public Collider2D CheckForColliderOverlap(Collider2D thisCollider)
    {
        // prepare list to put in as many overlaps as there are
        List<Collider2D> results = new List<Collider2D>();
        thisCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);

        foreach (Collider2D overlap in results)
        {
            if (overlap.TryGetComponent<Button>(out Button button)) return overlap; // ~~list nulls are apparently skipped better than array nulls?
        }

        return null;
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
