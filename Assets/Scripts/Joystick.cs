using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle;
    public RectTransform outLine;

    private float deadZone = 0;
    private float handleRange = 1;
    private Vector3 input = Vector3.zero;
    private Canvas canvas;

    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        outLine = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 radius = outLine.sizeDelta / 2;
        input = (eventData.position - outLine.anchoredPosition) / (radius * canvas.scaleFactor);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
