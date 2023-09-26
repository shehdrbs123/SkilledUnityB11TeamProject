using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    private PrefabManager prefabManager;
    private int day = 0;

    private void Awake()
    {
        prefabManager = GetComponent<PrefabManager>();
    }

    //private void Start()
    //{
    //    InvokeRepeating(nameof(TEST), 0f, 1f);
    //}

    //private void TEST()
    //{
    //    GameObject go = prefabManager.SpawnFromPool(PoolType.Monster);
    //    go.SetActive(true);
    //}
}
