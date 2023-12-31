﻿using System.Text;
using UnityEngine;

public abstract class GridScriptableObject : ScriptableObject
{
    public Sprite Image;
    public ItemData[] resoureces;
    public int[] resourecsCount;

    protected StringBuilder sb = new StringBuilder(100);
    public abstract string GetDataInfo();
    public abstract string GetName();
}
