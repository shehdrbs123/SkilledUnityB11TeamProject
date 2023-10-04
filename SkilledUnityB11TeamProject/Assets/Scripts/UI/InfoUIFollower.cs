using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InfoUIFollower : MonoBehaviour, IPointerMoveHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Vector2 _offset;
    //Todo
    //나중에 모든 UI 가져오는 방법은 Enum이나, const string 을 모아 놓은 class로 변경할까 고민중.
    [SerializeField] private string _InfoPanelName;
    [SerializeField] 
    private RectTransform _infoPanelTransform;
    private GameObject _InfoPanel;
    private GridButtonUI _buttonUI;
    private void Start()
    {
        UIManager _uiManager = GameManager.Instance._uiManager;
        _InfoPanel = _uiManager.GetUI(_InfoPanelName);
        _InfoPanel.SetActive(false);
        
        _infoPanelTransform = _InfoPanel.transform.GetChild(0).GetComponent<RectTransform>();
        _buttonUI = GetComponent<GridButtonUI>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        float xPos = eventData.position.x + (_infoPanelTransform.rect.width * 0.5f) + _offset.x;
        float yPos = eventData.position.y - (_infoPanelTransform.rect.height * 0.5f) - _offset.y;

        _infoPanelTransform.position = new Vector2(xPos, yPos);
    }

    private void OnDisable()
    {
        if(_InfoPanel)
            _InfoPanel.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _InfoPanel.gameObject.SetActive(true);
        _InfoPanel.GetComponent<ResourceInfoPanelUI>().Init(_buttonUI.GetResourceData());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _InfoPanel.gameObject.SetActive(false);
    }
}
