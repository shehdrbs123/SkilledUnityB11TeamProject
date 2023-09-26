using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pool
{
    public PoolType type;       // 풀링할 오브젝트
    public GameObject prefab;   // 프리팹
    public Transform holder;    // 오브젝트 생성 부모. PrefabMangaer 컴포넌트 아래에 새 게임오브젝트를 만들어서 달아주기. Hierarchy 정리용
}

public enum PoolType            // PoolType의 값만큼 오브젝트 풀링
{
    Monster = 30,               // 몬스터
    SFXAudio = 30,              // 효과음
}

public class PrefabManager : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<PoolType, Queue<GameObject>> poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

    private void Awake()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < (int)pool.type; i++)
            {
                GameObject _object = Instantiate(pool.prefab, pool.holder);
                _object.SetActive(false);
                objectPool.Enqueue(_object);
            }

            poolDictionary.Add(pool.type, objectPool);
        }
    }

    public GameObject SpawnFromPool(PoolType type)
    {
        if (!poolDictionary.ContainsKey(type))
            return null;

        GameObject _object = poolDictionary[type].Dequeue();
        poolDictionary[type].Enqueue(_object);

        return _object;
    }
}
