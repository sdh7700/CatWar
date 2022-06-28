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
  public Vector3 input = Vector3.zero;
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
    OnDrag(eventData);
  }

  public void OnDrag(PointerEventData eventData)
  {
    Vector2 radius = outLine.sizeDelta / 2;
    // 조이스틱 중앙에서 터치한 곳까지의 거리를 백분율로 나타냄
    input = (eventData.position - outLine.anchoredPosition) / (radius * canvas.scaleFactor);
    // 거리가 조이스틱을 넘어가거나 데드존에 진입할 경우 제어
    HandleInput(input.magnitude, input.normalized);

    handle.anchoredPosition = input * radius * handleRange;


  }

  private void HandleInput(float magnitude, Vector2 normalizedVector)
  {
    if (magnitude > deadZone)
    {
      if (magnitude > 1)
        input = normalizedVector;
    }
    else
      input = Vector2.zero;
  }


  public void OnPointerUp(PointerEventData eventData)
  {
    input = Vector2.zero;
    handle.anchoredPosition = Vector2.zero;
  }
}
