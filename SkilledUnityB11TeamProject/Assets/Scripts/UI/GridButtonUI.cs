using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class GridButtonUI : MonoBehaviour
{
   
    [SerializeField] protected Image _buildTargetImage;
    protected Button _button;
    protected Inventory _inventory;
    public abstract void Init(ScriptableObject data,Transform parent,UnityAction PanelOff);
    public abstract void UpdateButton();
    public abstract GridScriptableObject GetResourceData();

    protected void ButtonEnable(bool enable)
    {
        _button.interactable = enable;
    }

    protected void SetImage(Sprite sprite)
    {
        _buildTargetImage.sprite = sprite;
    }
}
