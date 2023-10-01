using System.Text;
using UnityEngine;

public abstract class GridScriptableObject : ScriptableObject
{
    public Sprite Image;
    public ItemData[] resoureces;
    public int[] resourecsCount;

    protected StringBuilder sb = new StringBuilder();
    public abstract string GetDataInfo();
}
