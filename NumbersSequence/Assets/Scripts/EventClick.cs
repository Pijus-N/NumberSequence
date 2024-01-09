using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject clickedPoint = eventData.pointerEnter.gameObject;
        Actions.OnPointClicked(clickedPoint.GetComponent<Point>().GetNumber());
    }

}
