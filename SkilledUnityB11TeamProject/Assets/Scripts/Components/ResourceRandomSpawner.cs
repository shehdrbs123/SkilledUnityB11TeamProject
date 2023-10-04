using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRandomSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject resourcePrefab;
    [SerializeField] private int resourceCount;

    [SerializeField] protected float baseRespawnDelay;
    [SerializeField] protected float randomRespawnDelay;

    [SerializeField] protected int baseCapacity;
    [SerializeField] private int randomCapacity;

    [SerializeField] private GameObject range;

    private void Start()
    {
        range.SetActive(false);

        for (int i = 0; i < resourceCount; i++)
        {
            Instantiate(resourcePrefab, RandomPosition(), Quaternion.identity, transform);
        }
    }

    public int InitCapacity()
    {
        return baseCapacity + Random.Range(0, randomCapacity);
    }

    private Vector3 RandomPosition()
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * range.transform.localScale.x / 2;
        pos.y = 0;

        return pos;
    }

    public void Respawn(GameObject obj)
    {
        StartCoroutine(CoRespawn(obj));
    }

    private IEnumerator CoRespawn(GameObject obj)
    {
        yield return new WaitForSeconds(baseRespawnDelay + Random.Range(0, randomRespawnDelay));

        obj.transform.position = RandomPosition();
        obj.SetActive(true);
    }
}
