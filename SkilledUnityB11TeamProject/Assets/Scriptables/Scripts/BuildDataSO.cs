using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BuildDataSO", menuName = "Scriptable/Build Data")]
public class BuildDataSO : ScriptableObject
{
    public Sprite StructureUISprite;
    public GameObject StructurePrefab;
    
    //private ItemData[] resoureces
    //private int[] resourecsCount;
}
