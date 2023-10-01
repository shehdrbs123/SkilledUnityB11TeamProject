using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CraftManager : GridPanelManager
{
    private Dictionary<GridPanelType, CraftDataSO[]> dicRecipes;
    
    
    private void Awake()
    {
        dicRecipes = new Dictionary<GridPanelType, CraftDataSO[]>();
        var craft = Resources.LoadAll<CraftDataSO>(Path.Combine("CraftData","WaterCraft"));
        dicRecipes.Add(GridPanelType.WaterCraft,craft);
        craft = Resources.LoadAll<CraftDataSO>(Path.Combine("CraftData","FoodCraft"));
        dicRecipes.Add(GridPanelType.FoodCraft,craft);
    }

    public override int GetElementsCount(GridPanelType type)
    {
        return dicRecipes[type].Length;
    }

    public override ScriptableObject GetData(GridPanelType type, int idx)
    {
        return dicRecipes[type][idx];
    }
}
