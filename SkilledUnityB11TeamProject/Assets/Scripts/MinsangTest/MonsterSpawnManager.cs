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
}
