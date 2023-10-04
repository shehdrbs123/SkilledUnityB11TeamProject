using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftButtonUI : GridButtonUI
{
    [SerializeField] private AudioClip uiSound;
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
    
    private void MakeItem()
    {
        GameObject player = GameManager.Instance.GetPlayer();
        Inventory inven = player.GetComponent<Inventory>();
        for (int i = 0; i < DataSo.resoureces.Length; ++i)
        {
            inven.ComsumeItem(DataSo.resoureces[i],DataSo.resourecsCount[i]);
        }
        inven.AddItem(DataSo.ResultItem);
        _updateAllButtons?.Invoke();
    }
    private void UpdateData()
    {
        _buildTargetImage.sprite = DataSo.Image;
    }

    public override void Init(ScriptableObject data, Transform parentContent, UnityAction PanelOff, UnityAction UpdateButtons)
    {
        base.Init(data,parentContent, PanelOff, UpdateButtons);
        DataSo= data as CraftDataSO;
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(PlaySound);
        _button.onClick.AddListener(MakeItem);
        transform.SetParent(parentContent,false);
        transform.localScale = Vector3.one;

        _updateAllButtons = UpdateButtons;
        UpdateButton();
    }

    public override GridScriptableObject GetResourceData()
    {
        return _data;
    }

    private void PlaySound()
    {
        SoundManager.PlayClip(uiSound,GameManager.Instance.GetPlayer().transform.position);
    }
    
}
