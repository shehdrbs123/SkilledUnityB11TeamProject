using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable/Bullet Data")]
public class BulletDataSO : ScriptableObject
{
    public LayerMask targetLayerMask;
    public float explosionRadius;
    public float damage;
    public AudioClip[] HitSound;
}
