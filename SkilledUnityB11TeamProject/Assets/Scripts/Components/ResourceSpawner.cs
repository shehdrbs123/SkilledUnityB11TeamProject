using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject resourcePrefab;
    [SerializeField] protected float respawnDelay;
    [SerializeField] protected int capacity;

    public abstract int InitCapacity();

    public abstract void Respawn(GameObject obj);
}
