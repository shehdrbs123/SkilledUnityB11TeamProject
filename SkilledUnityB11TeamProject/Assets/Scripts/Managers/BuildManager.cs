using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
     private BuildDataSO[] buildDatas;

     private GameObject Player;
     private void Awake()
     {
          buildDatas = Resources.LoadAll<BuildDataSO>("StructureData");
     }

     public BuildDataSO GetBuildData(int idx)
     {
          return buildDatas[idx];
     }

     public int GetBuildDataCount()
     {
          return buildDatas.Length;
     }

     public void SetBuildMode(BuildDataSO data)
     {
          
     }
}
