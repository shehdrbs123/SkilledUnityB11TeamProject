using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridPanelUI : BaseUI
{
    public string ButtonUIName;
    
    [SerializeField] private Transform _contentPanel;
    
    private BuildManager _buildManager;
    private UIManager _uiManager;
    //private List<GridButtonUI> buttonUI;

    protected override void Awake()
    {
        base.Awake();
        //buttonUI = new List<GridButtonUI>(20);
    }
    private void Start()
    {
        _uiManager = GameManager.Instance._uiManager;
        _buildManager = GameManager.Instance._buildManager;
        int count = _buildManager.GetBuildDataCount();
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = _uiManager.GetUIClone(ButtonUIName);
            obj.GetComponent<GridButtonUI>().Init(_buildManager.GetBuildData(i),_contentPanel);
        }
    }
}
