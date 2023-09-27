using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    private DayManager dayManager;
    private PrefabManager prefabManager;

    [SerializeField] private List<MonsterDataSO> monsterDatas;
    
    public int spawnCount = 5;
    public int spawnDelay = 3;

    private Coroutine now = null;

    private void Awake()
    {
        prefabManager = GameManager.Instance.prefabManager;
        dayManager = GameManager.Instance._dayManager;
    }

    private void Update()
    {
        if (dayManager.isNight && now == null)
        {
            spawnCount += dayManager.day;
            now = StartCoroutine(CoSpawn());
        }
        else if (!dayManager.isNight && now != null)
        {
            StopCoroutine(now);
            now = null;
        }
    }

    private IEnumerator CoSpawn()
    {
        WaitForSeconds delay = new WaitForSeconds(spawnDelay);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject mon = prefabManager.SpawnFromPool(PoolType.Monster);
            mon.GetComponent<Monster>().data = monsterDatas[i % 3];
            mon.SetActive(true);

            yield return delay;
        }
    }
}
