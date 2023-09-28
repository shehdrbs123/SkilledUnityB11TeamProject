using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[RequireComponent(typeof(Rigidbody))]
public class TurretsAI : TurretAIBase
{
    protected override void OperateAttack()
    {
        var mon = _enemys[0].GetComponent<Monster>();
        mon.Hit(_data.Damage, out bool isDie);
        _currentAttackWait = 0;
        _animator.SetTrigger(_attackAniHash);
        Array.ForEach(_paricles,(x)=>x.Play());
        if (isDie)
            _enemys.Remove(mon.gameObject);
    }

    protected override void LookAtEnemy()
    {
        Vector3 enemyPosition = new Vector3(_enemys[0].transform.position.x, 0, _enemys[0].transform.position.z);
        _head.LookAt(enemyPosition);
    }
}
