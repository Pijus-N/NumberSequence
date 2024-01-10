using UnityEngine;

public class CanvasOverlapDetector : MonoBehaviour
{
    [SerializeField]private Canvas canvas;
    [SerializeField] private float overLapBoxSizeWidth = 1.5f;
    /// <summary>
    /// Check if point number canvas overlaps with other points
    /// </summary>
    /// <returns></returns>
    public bool CanvasOverlaps2DObjects()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector3 canvasCenter = canvasRect.position;
        Vector3 boxSize = new Vector3(canvasRect.rect.width/ overLapBoxSizeWidth, canvasRect.rect.height, 0f);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(canvasCenter, boxSize, 0f);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Point"))
            {
                return true;
            }
        }
        return false;
    }



}