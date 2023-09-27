using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrattTable : MonoBehaviour, IInteractable
{
    private UIManager _uimanager;

    private void Start()
    {
        _uimanager = GameManager.Instance._uiManager;
    }


    public string GetInteractPrompt()
    {
        return "Open Craft";
    }

    public void OnInteract()
    {
        GameObject panel = _uimanager.GetUI("GridPanelUI");
        GridPanelUI panelUI = panel.GetComponent<GridPanelUI>();
        
        panelUI.Init();
    }
}
