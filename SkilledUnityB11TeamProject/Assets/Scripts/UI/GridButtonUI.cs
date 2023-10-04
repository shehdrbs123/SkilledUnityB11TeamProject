using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class GridButtonUI : MonoBehaviour
{
   
    [SerializeField] protected Image _buildTargetImage;
    protected Button _button;
    protected Inventory _inventory;
    protected UnityAction _updateAllButtons;

    public virtual void Init(ScriptableObject data, Transform parentContent, UnityAction PanelOff,
        UnityAction UpdateButtons)
    {
        _updateAllButtons = UpdateButtons;
    }
    
    public abstract GridScriptableObject GetResourceData();

    protected void ButtonEnable(bool enable)
    {
        _button.interactable = enable;
    }

    protected void SetImage(Sprite sprite)
    {
        _buildTargetImage.sprite = sprite;
    }

    public void UpdateButton()
    {
        if (!_inventory)
            _inventory = GameManager.Instance.GetPlayer().GetComponent<Inventory>();
        for (int i = 0; i < GetResourceData().resoureces.Length; ++i)
        {
            if(!_inventory.HasItems(GetResourceData().resoureces[i],GetResourceData().resourecsCount[i]))
            {
                ButtonEnable(false);
                return;
            }
        }
        ButtonEnable(true);      
    }
}
