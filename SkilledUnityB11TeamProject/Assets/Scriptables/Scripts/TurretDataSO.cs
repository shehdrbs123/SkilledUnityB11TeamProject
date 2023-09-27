using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretData", menuName = "Scriptable/TurretData")]
public class TurretDataSO : ScriptableObject
{
    public float range;
    public int Damage;
    public float attackRate;

}
