using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pool
{
    public PoolType type;       // 풀링할 오브젝트
    public GameObject prefab;   // 프리팹
    public int count;           // 오브젝트 풀링 갯수
    public Transform holder;    // 오브젝트 생성 부모. PrefabMangaer 컴포넌트 아래에 새 게임오브젝트를 만들어서 달아주기. Hierarchy 정리용

    public Pool(PoolType _type, GameObject _prefab, int _count, Transform _holder)
    {
        type = _type;
        prefab = _prefab;
        count = _count;
        holder = _holder;
    }
}

public enum PoolType
{
    M_Spider,              // 몬스터 - 거미
    SFXAudio,              // 효과음
    Battery,               // 배터리
    BangParticle,
}

public class PrefabManager : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<PoolType, Queue<GameObject>> poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

    private void Awake()
    {
        foreach (Pool pool in pools)
        {
            AddOnDictionary(pool);
        }
    }

    private void AddOnDictionary(Pool pool)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < pool.count; i++)
        {
            GameObject _object = Instantiate(pool.prefab, pool.holder);
            _object.SetActive(false);
            objectPool.Enqueue(_object);
        }

        poolDictionary.Add(pool.type, objectPool);
    }

    public void AddAtPoolsOnScript(PoolType _type, GameObject _object, int _count)      // holder 지정해주지 않으면 GameManager.PrefabManager 하위에 홀더가 생김
    {
        Transform tf = new GameObject("Holder").transform;
        tf.SetParent(transform);

        Pool newPool = new Pool(_type, _object, _count, tf);

        pools.Add(newPool);
        AddOnDictionary(newPool);
    }

    public void AddAtPoolsOnScript(PoolType type, GameObject obj, int _count, Transform holder)
    {
        Pool newPool = new Pool(type, obj, _count, holder);

        pools.Add(newPool);
        AddOnDictionary(newPool);
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
