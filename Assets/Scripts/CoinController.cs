using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoinController : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;

    private Vector3 screenPosition;
    private Vector3 mousePositionOnScreen;
    private Vector3 mousePositionInWorld;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {

        screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        mousePositionOnScreen = Input.mousePosition;

        mousePositionOnScreen.z = screenPosition.z;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        transform.position = mousePositionInWorld;

    }

}
