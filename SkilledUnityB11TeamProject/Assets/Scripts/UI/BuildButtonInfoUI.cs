using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BuildButtonInfoUI : MonoBehaviour, IPointerMoveHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private RectTransform InfoPanel;
    [SerializeField] private Vector2 offset;

    public void OnPointerMove(PointerEventData eventData)
    {
        float xPos = eventData.position.x + (InfoPanel.rect.width * 0.5f) + offset.x;
        float yPos = eventData.position.y - (InfoPanel.rect.height * 0.5f) - offset.y;

        InfoPanel.transform.position = new Vector2(xPos, yPos);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoPanel.gameObject.SetActive(false);
    }
}
