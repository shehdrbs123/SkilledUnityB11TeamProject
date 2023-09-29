
using System;
using UnityEngine;

public abstract class GridPanelManager : MonoBehaviour
{
    public abstract int GetElementsCount();
    public abstract ScriptableObject GetData(int idx);
    public Action OnOperated;
}
