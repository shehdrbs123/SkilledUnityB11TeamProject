using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRandomSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject resourcePrefab;
    [SerializeField] protected float respawnDelay;
    [SerializeField] protected float respawnRandomDelay;
    [SerializeField] protected int capacity;

    [Header("Random Spawner")]
    [SerializeField] private int resourceCount;
    [SerializeField] private int randomCapacity;

    [SerializeField] private GameObject range;
    private float radius;

    private void Start()
    {
        radius = range.transform.localScale.x / 2;
        range.SetActive(false);

        for (int i = 0; i < resourceCount; i++)
        {
            Instantiate(resourcePrefab, RandomPosition(), Quaternion.identity, transform);
        }
    }

    public int InitCapacity()
    {
        return capacity + Random.Range(0, randomCapacity);
    }

    private Vector3 RandomPosition()
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * radius;
        pos.y = 0;

        return pos;
    }

    public void Respawn(GameObject obj)
    {
        StartCoroutine(CoRespawn(obj));
    }

    private IEnumerator CoRespawn(GameObject obj)
    {
        yield return new WaitForSeconds(respawnDelay + Random.Range(0, respawnRandomDelay));

        obj.transform.position = RandomPosition();
        obj.SetActive(true);
    }
}
