using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFixSpawner : ResourceSpawner
{
    private void Start()
    {
        Instantiate(resourcePrefab, transform);
    }

    public override int InitCapacity()
    {
        return capacity;
    }

    public override void Respawn(GameObject obj)
    {
        StartCoroutine(CoRespawn(obj));
    }

    private IEnumerator CoRespawn(GameObject obj)
    {
        yield return new WaitForSeconds(respawnDelay + Random.Range(0, respawnRandomDelay));

        obj.SetActive(true);
    }
}
