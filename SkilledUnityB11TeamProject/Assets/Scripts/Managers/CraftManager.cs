using System;
using UnityEngine;

public class CraftManager : GridPanelManager
{

    private CraftDataSO[] craftRecipes;

    private void Awake()
    {
        craftRecipes = Resources.LoadAll<CraftDataSO>("CraftData");
    }

    public override int GetElementsCount()
    {
        return craftRecipes.Length;
    }

    public override ScriptableObject GetData(int idx)
    {
        return craftRecipes[idx];
    }
}
