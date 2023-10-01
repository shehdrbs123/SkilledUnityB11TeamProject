using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    private DayManager dayManager;
    private PrefabManager prefabManager;

    [SerializeField] private MonsterWaveSO wave;

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
        Wave nowWave = wave.waves[dayManager.day - 1];

        WaitForSeconds delay = new WaitForSeconds(nowWave.spawnDelay);

        for (int i = 0; i < nowWave.monsters.Count; i++)
        {
            for (int j = 0; j < nowWave.monsterCount[i]; j++)
            {
                GameObject mon = prefabManager.SpawnFromPool(nowWave.type);
                mon.GetComponent<Monster>().data = nowWave.monsters[i];
                mon.SetActive(true);

                yield return delay;
            }
        }
    }
}
