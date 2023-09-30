using System;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable/TurretData")]
[Serializable]
public class TurretDataSO : ScriptableObject
{
    public float halfRadius;
    public int Damage;
    public float attackRate;
    public AudioClip[] _moveSound;
    public AudioClip[] _shotSound;
}
