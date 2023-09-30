using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable/TurretData")]
public class TurretDataSO : ScriptableObject
{
    [FormerlySerializedAs("range")] public float halfRadius;
    public int Damage;
    public float attackRate;
    public AudioClip[] _moveSound;
    public AudioClip[] _shotSound;
}
