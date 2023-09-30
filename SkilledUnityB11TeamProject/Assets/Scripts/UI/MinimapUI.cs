using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Minimap : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private RectTransform MinimapRect;
    [SerializeField] private float largeScale;
    [SerializeField] private float changingTime;
    private UIManager _uiManager;
    
    private void Start()
    {
        _uiManager = GameManager.Instance._uiManager;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(_uiManager.LerpAdjustRect(MinimapRect,largeScale,largeScale,changingTime));
    }

    

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(_uiManager.LerpAdjustRect(MinimapRect,1,1,changingTime));
    }
}
