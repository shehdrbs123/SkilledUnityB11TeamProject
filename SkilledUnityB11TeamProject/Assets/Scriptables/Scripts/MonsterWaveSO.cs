using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public PoolType type;
    public List<MonsterDataSO> monsters;
    public float spawnDelay;
}

[CreateAssetMenu(fileName = "MonsterWave", menuName = "Scriptable/Monster Wave")]
public class MonsterWaveSO : ScriptableObject
{
    public List<Wave> waves;
}
