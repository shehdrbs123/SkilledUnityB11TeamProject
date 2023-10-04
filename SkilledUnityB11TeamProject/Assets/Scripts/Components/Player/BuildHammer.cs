using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHammer : EquipTool
{
    private BuildManager _buildManager;
    private UIManager _uiManager;
    
    protected override void Awake()
    {
        base.Awake();
        _buildManager = GameManager.Instance._buildManager;
        _uiManager = GameManager.Instance._uiManager;
    }

    public override void OnFire2Input()
    {
        if (!_buildManager.isBuildMode)
        {
            GameObject obj = _uiManager.GetUI("BuildPanelUI");
            GridPanelUI panel = obj.GetComponent<GridPanelUI>();
            panel.Init();
            obj.SetActive(true);
        }
    }
}
