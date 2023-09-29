using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject resourcePrefab;
    public float respawnTime;

    private void Start()
    {
        Instantiate(resourcePrefab, transform);
    }

    public void Respawn()
    {
        StartCoroutine(CoRespawn());
    }

    private IEnumerator CoRespawn()
    {
        yield return new WaitForSeconds(Random.Range(respawnTime, respawnTime + 3f));

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
