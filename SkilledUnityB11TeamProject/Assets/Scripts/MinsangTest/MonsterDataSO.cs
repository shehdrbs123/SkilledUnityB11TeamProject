using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable/Monster Data")]
public class MonsterDataSO : ScriptableObject
{
    [Header("Info")]
    public Material material;
    public int hp;
    public float speed;
    public readonly Vector3 spawnPosition = new Vector3(-85, 0, 105);
    public readonly Vector3 targetPosition = new Vector3(45, 0, -85);
    public GameObject[] dropResources;
}
