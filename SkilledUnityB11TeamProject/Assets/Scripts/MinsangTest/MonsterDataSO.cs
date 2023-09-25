using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable/Monster Data")]
public class MonsterDataSO : ScriptableObject
{
    [Header("Info")]
    public int hp;
    public float speed;
    public Vector3 spawnPosition;
    public Vector3 targetPosition;
    public GameObject[] dropResources;
}
