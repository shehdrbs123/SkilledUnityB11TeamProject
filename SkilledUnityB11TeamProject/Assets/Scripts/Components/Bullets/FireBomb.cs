using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Bullets
{
    [SerializeField] private PoolType SpawnDamageField;
    protected override void Explosion()
    {
        GameObject DamageRange = _prefabManager.SpawnFromPool(SpawnDamageField);
        DamageRange.transform.position = transform.position;
        DamageRange.SetActive(true);
    }
}
