using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
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
    private List<GridButtonUI> buttons;
    public void Init()
    {
        if (manager == null)
        {
            InitValues();
            int count = manager.GetElementsCount();
            buttons = new List<GridButtonUI>(20);
            for (int i = 0; i < count; ++i)
            {
                if (_uiManager == null)
                    _uiManager = GameManager.Instance._uiManager;
                GameObject obj = _uiManager.GetUIClone(buttonUIName);
                GridButtonUI gridButtonUI = obj.GetComponent<GridButtonUI>();
                gridButtonUI.Init(manager.GetData(i),_contentPanel, () => gameObject.SetActive(false));
                buttons.Add(gridButtonUI);
            }
        }
        else
        {
            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        foreach (GridButtonUI button in buttons)
        {
            button.UpdateButton();
        }
    }

    private void InitValues()
    {
        switch (PanelType)
        {
            case GridPanelType.Craft:
                manager = GameManager.Instance._craftManager;
                buttonUIName = "CraftButtonUI";
                manager.OnOperated += UpdateButtons;
                break;
            case GridPanelType.Build:
                manager = GameManager.Instance._buildManager;
                buttonUIName = "BuildSttButtonUI";
                manager.OnOperated += UpdateButtons;
                break;
        }
        
    }
}
