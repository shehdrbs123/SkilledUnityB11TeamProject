
using System;
using UnityEngine;

public abstract class GridPanelManager : MonoBehaviour
{
    public abstract int GetElementsCount(GridPanelType type);
    public abstract ScriptableObject GetData(GridPanelType type, int idx);
    public Action OnOperated;
}
