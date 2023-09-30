using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageRange", menuName = "Scriptable/DamageRange")]
public class DamageRangeDataSO : BulletDataSO
{
    public float damageRate;
    public float duration;
}
