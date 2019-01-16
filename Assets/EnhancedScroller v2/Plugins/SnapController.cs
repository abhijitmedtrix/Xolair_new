using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.EventSystems;

public class SnapController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private EnhancedScroller scroller;

    void Awake()
    {
        scroller = GetComponent<EnhancedScroller>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        scroller.snapping = false;
    }

    public void OnEndDrag(PointerEventData data)
    {
        scroller.snapping = true;
    }
}