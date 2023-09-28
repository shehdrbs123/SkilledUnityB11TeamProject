using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildTargetButtonUI : GridButtonUI
{
    public BuildDataSO dataSo
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
    private BuildDataSO _data;

    private BuildManager _buildManager;
    
    private void Start()
    {
        _buildManager = GameManager.Instance._buildManager;
    }

    public void CreateBuild()
    {
        _buildManager.SetBuildMode(dataSo);
    }

    public void UpdateData()
    {
        SetImage(dataSo.Image);
    }

    public override void Init(ScriptableObject data, Transform parent, UnityAction PanelOff)
    {
        _button = GetComponent<Button>();

        dataSo = data as BuildDataSO;
        _button.onClick.AddListener(CreateBuild);
        _button.onClick.AddListener(PanelOff);
            
        transform.SetParent(parent,false);
        transform.localScale = Vector3.one;
        
        UpdateButton();
    }

    public override void UpdateButton()
    {
        for (int i = 0; i < dataSo.resoureces.Length; ++i)
        {
            if(!_inventory.HasItems(dataSo.resoureces[i],dataSo.resourecsCount[i]))
            {
                ButtonEnable(false);
                return;
            }
        }
        ButtonEnable(true);        
    }
}
