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
    
    protected void Start()
    {
        _buildManager = GameManager.Instance._buildManager;
    }

    public void CreateBuild()
    {
        _buildManager.SetBuildMode(dataSo);
        _buildManager.OnOperated += ComsumeItem;
    }

    private void ComsumeItem()
    {
        for (int i = 0; i < dataSo.resoureces.Length; ++i)
        {
            _inventory.ComsumeItem(dataSo.resoureces[i],dataSo.resourecsCount[i]);
        }
        
        _buildManager.OnOperated -= ComsumeItem;
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

    

    public override GridScriptableObject GetResourceData()
    {
        return _data;
    }
}
