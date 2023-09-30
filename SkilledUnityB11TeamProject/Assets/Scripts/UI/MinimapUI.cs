using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minimap : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private RectTransform MinimapRect;
    [SerializeField] private float size;
    [SerializeField] private float changingTime;
    private float defaultSize;

    private void Awake()
    {
        defaultSize = MinimapRect.rect.width;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AdJustPanel(size));
    }

    private IEnumerator AdJustPanel(float size)
    {
        float currentTime = 0;
        Vector2 targetSize = new Vector2(size, size);
        float deltaSize = Time.deltaTime / changingTime;
        while (currentTime <= changingTime)
        {
            MinimapRect.sizeDelta = Vector2.Lerp(MinimapRect.sizeDelta, targetSize, deltaSize); 
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AdJustPanel(defaultSize));
    }
}
