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

    public override void Init(ScriptableObject data, Transform parent, UnityAction PanelOff)
    {
        BuildTargetButtonUI target = GetComponent<BuildTargetButtonUI>();
        _button = GetComponent<Button>();

        target.dataSo = data as BuildDataSO;
        _button.onClick.AddListener(target.CreateBuild);
        _button.onClick.AddListener(PanelOff);
            
        transform.SetParent(parent,false);
        transform.localScale = Vector3.one;
    }


    public void CreateBuild()
    {
        _buildManager.SetBuildMode(dataSo);
    }

    public void UpdateData()
    {
        SetImage(dataSo.Image);
    }
}
