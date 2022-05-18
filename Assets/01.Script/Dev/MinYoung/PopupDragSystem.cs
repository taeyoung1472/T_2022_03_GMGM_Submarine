using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupDragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform rect;
    public int a;
    Vector3 eventPos;
    bool isDraging;
    public void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }
    public void Update()
    {
        if (isDraging)
        {
            rect.position = Vector3.Lerp(rect.position, eventPos, Time.deltaTime * 10f);//eventData.position;
        }
    }
}
