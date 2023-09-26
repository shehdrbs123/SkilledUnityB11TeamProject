using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelUI : BaseUI
{
    [SerializeField] private Transform _contentPanel;
    
    private BuildManager _buildManager;
    private UIManager _uiManager;
    private List<BuildTargetButtonUI> buttonUI;

    protected override void Awake()
    {
        base.Awake();
        buttonUI = new List<BuildTargetButtonUI>(20);
    }
    private void Start()
    {
        _buildManager = GameManager.Instance._buildManager;
        _uiManager = GameManager.Instance._uiManager;        
        int count = _buildManager.GetBuildDataCount();
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = _uiManager.GetUIClone("BuildSttButtonUI");
            BuildTargetButtonUI target = obj.GetComponent<BuildTargetButtonUI>();
            Button button = obj.GetComponent<Button>();
            BuildDataSO buildData = _buildManager.GetBuildData(i);
            
            target.DataSo = buildData;
            button.onClick.AddListener(()=>{gameObject.SetActive(false);});
            button.onClick.AddListener(target.CreateBuild);
            
            obj.transform.SetParent(_contentPanel,false);
        }
    }
}
