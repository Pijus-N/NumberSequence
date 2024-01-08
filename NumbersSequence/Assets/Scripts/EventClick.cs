using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject clickedPoint = eventData.pointerEnter.gameObject;
        Actions.OnPointClicked(clickedPoint.GetComponent<Point>().GetNumber());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(eventData.position);
    }
}
