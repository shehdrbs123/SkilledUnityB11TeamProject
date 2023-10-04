using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CraftTable : MonoBehaviour, IInteractable
{
    private UIManager _uimanager;

    [SerializeField] private GridPanelType CraftType;
    private void Start()
    {
        _uimanager = GameManager.Instance._uiManager;
    }


    public string GetInteractPrompt()
    {
        switch (CraftType)
        {
            case GridPanelType.FoodCraft :
                return "음식 제작기";
            case GridPanelType.WaterCraft :
                return "음료 제작기";
        }

        return string.Empty;
    }

    public void OnInteract()
    {
        GameObject panel = _uimanager.GetUI(GetPanelName());
        GridPanelUI panelUI = panel.GetComponent<GridPanelUI>();
        panelUI.Init();
        panel.SetActive(true);
    }

    public void PlayInteractionSound()
    {
    }

    private string GetPanelName()
    {
        switch (CraftType)
        {
            case GridPanelType.FoodCraft :
                return "FoodCraftPanelUI";
            case GridPanelType.WaterCraft :
                return "WaterCraftPanelUI";
        }
        return null;
    }
}
