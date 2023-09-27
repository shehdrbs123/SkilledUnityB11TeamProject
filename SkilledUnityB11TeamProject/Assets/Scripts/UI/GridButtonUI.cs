using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class GridButtonUI : MonoBehaviour
{
   
    [SerializeField] protected Image _buildTargetImage;
    protected Button _button;
    public abstract void Init(ScriptableObject data,Transform parent,UnityAction PanelOff);
    
    private void ButtonEnable(bool enable)
    {
        _button.interactable = enable;
    }

    protected void SetImage(Sprite sprite)
    {
        _buildTargetImage.sprite = sprite;
    }
}
