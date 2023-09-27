using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BuildData", menuName = "Scriptable/Build Data")]
public class BuildDataSO : GridScriptableObject
{
    public GameObject StructurePrefab;

    private ItemData[] resoureces;
    private int[] resourecsCount;
}
