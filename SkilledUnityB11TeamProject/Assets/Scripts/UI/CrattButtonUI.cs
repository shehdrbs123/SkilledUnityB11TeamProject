using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrattButtonUI : GridButtonUI
{
    public CraftDataSO DataSo
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
            UpdateData();
        }
    }

    private CraftDataSO _data;
    
    public override void Init(ScriptableObject data, Transform parent, UnityAction PanelOff)
    {
        DataSo= data as CraftDataSO;
        
        _button.onClick.AddListener(PanelOff);
       // _button.onClick.AddListener(MakeItem);
        
        transform.SetParent(parent);
    }

    private void MakeItem()
    {
        GameObject player = GameManager.Instance.GetPlayer();
        Inventory inven = player.GetComponent<Inventory>();
        inven.AddItem(_data.ResultItem);
    }
    private void UpdateData()
    {
        _buildTargetImage.sprite = _data.Image;
    }
}
