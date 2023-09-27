using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum GridPanelType
{
    Build=0, Craft
}

public class GridPanelUI : BaseUI
{
    public GridPanelType PanelType;
    
    [SerializeField] private Transform _contentPanel;
    
    private string buttonUIName;
    private GridPanelManager manager;

    public void Init()
    {
        if (manager == null)
        {
            InitValues();
            int count = manager.GetElementsCount();
            for (int i = 0; i < count; ++i)
            {
                GameObject obj = _uiManager.GetUIClone(buttonUIName);
                obj.GetComponent<GridButtonUI>().Init(manager.GetData(i),_contentPanel,() => gameObject.SetActive(false));
            }
        }
    }

    private void InitValues()
    {
        switch (PanelType)
        {
            case GridPanelType.Craft:
                manager = GameManager.Instance._craftManager;
                buttonUIName = "CraftButtonUI";
                break;
            case GridPanelType.Build:
                manager = GameManager.Instance._buildManager;
                buttonUIName = "BuildSttButtonUI";
                break;
        }
        
    }
}
