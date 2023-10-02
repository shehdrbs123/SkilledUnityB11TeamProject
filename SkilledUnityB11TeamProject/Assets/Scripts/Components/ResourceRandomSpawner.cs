using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRandomSpawner : ResourceSpawner
{
    [Header("Random Spawner")]
    [SerializeField] private float radius;
    [SerializeField] private int resourceCount;
    [SerializeField] private int randomCapacity;

    private void Start()
    {
        for (int i = 0; i < resourceCount; i++)
        {
            Instantiate(resourcePrefab, RandomPosition(), Quaternion.identity, transform);
        }
    }

    public override int InitCapacity()
    {
        return capacity + Random.Range(0, randomCapacity);
    }

    private Vector3 RandomPosition()
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * radius;
        pos.y = 0;

        return pos;
    }

    public override void Respawn(GameObject obj)
    {
        StartCoroutine(CoRespawn(obj));
    }

    private IEnumerator CoRespawn(GameObject obj)
    {
        yield return new WaitForSeconds(respawnDelay + Random.Range(0, 5f));

        obj.transform.position = RandomPosition();
        obj.SetActive(true);
    }
}
