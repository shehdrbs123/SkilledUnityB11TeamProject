using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFixSpawner : ResourceSpawner
{
    public GameObject resourcePrefab;
    public float respawnTime;

    private void Start()
    {
        Instantiate(resourcePrefab, transform);
    }

    public override void Respawn(GameObject obj)
    {
        StartCoroutine(CoRespawn(obj));
    }

    private IEnumerator CoRespawn(GameObject obj)
    {
        yield return new WaitForSeconds(respawnTime + Random.Range(0, 5f));

        obj.gameObject.SetActive(true);
    }
}
