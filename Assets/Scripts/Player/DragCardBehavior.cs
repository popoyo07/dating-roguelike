using UnityEngine;
using UnityEngine.EventSystems;

// Attach this to the UI element that receives pointer events (the card Image object).
// The script will move the parent RectTransform (CardRoot) so the whole card can be relocated.
public class DragCardBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform parentRect;      // CardRoot rect (the parent we will move)
    RectTransform selfRect;        // This object's rect
    Canvas rootCanvas;
    CanvasGroup parentCanvasGroup; // optional, on parent for raycast blocking
    public bool dragging;
    void Awake()
    {
        selfRect = GetComponent<RectTransform>();
        // parentRect is the direct parent; if hierarchy differs, change as needed
        if (transform.parent != null)
            parentRect = transform.parent.GetComponent<RectTransform>();

        // find the canvas this UI belongs to (used to scale pointer delta)
        rootCanvas = GetComponentInParent<Canvas>();

        // Try to get/create CanvasGroup on parent to control blocking raycasts while dragging
        if (parentRect != null)
        {
            parentCanvasGroup = parentRect.GetComponent<CanvasGroup>();
            if (parentCanvasGroup == null)
            {
                parentCanvasGroup = parentRect.gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;    
        // optional: lower raycast blocking so drop targets can detect pointer while dragging
        if (parentCanvasGroup != null)
            parentCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (parentRect == null)
        {
            // If parentRect wasn't found, fallback to moving this rect
            float scale = rootCanvas ? rootCanvas.scaleFactor : 1f;
            selfRect.anchoredPosition += eventData.delta / scale;
            return;
        }

        // Convert pointer delta to anchoredPosition delta using canvas scaleFactor
        float canvasScale = (rootCanvas != null) ? rootCanvas.scaleFactor : 1f;

        // Move parent anchored position by the pointer delta (UI coordinates)
        parentRect.anchoredPosition += eventData.delta / canvasScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        // Re-enable raycast blocking
        if (parentCanvasGroup != null)
            parentCanvasGroup.blocksRaycasts = true;
    }
}
