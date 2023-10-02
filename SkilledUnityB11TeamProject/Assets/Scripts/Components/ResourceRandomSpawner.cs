using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRandomSpawner : ResourceSpawner
{
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private float radius;
    [SerializeField] private int count;
    [SerializeField] private float respawnDelay;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _obj = Instantiate(resourcePrefab, RandomPosition(), Quaternion.identity, transform);
        }
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
        obj.gameObject.SetActive(true);
    }
}
