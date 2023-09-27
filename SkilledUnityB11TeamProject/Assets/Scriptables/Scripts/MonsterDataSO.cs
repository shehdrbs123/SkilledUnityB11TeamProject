using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable/Monster Data")]
public class MonsterDataSO : ScriptableObject
{
    public readonly Vector3 SPAWN_POSITION = new Vector3(-70, 0, 85);
    public readonly Vector3 TARGET_POSITION = new Vector3(35, 0, -65);

    public readonly WaitForSeconds DELAY_HIT = new WaitForSeconds(0.75f);
    public readonly WaitForSeconds DELAY_DIE = new WaitForSeconds(1f);
    public readonly WaitForSeconds DELAY_ATTACK = new WaitForSeconds(0.833f);

    public readonly int ANIM_DIE = Animator.StringToHash("IsDie");
    public readonly int ANIM_HIT = Animator.StringToHash("IsHit");
    public readonly int ANIM_ATTACK = Animator.StringToHash("IsAttack");

    [Header("Info")]
    public Material material;
    public int hp;
    public float speed;
    public GameObject[] dropResources;
}
