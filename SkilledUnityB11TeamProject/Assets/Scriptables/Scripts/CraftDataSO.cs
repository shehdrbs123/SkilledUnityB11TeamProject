using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CraftData", menuName = "Scriptable/Craft Recipe")]
public class CraftDataSO : GridScriptableObject
{
    public ItemData[] Resources;
    public int[] ResourceCount;
    public ItemData ResultItem;
}
