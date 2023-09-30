using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class TurretsAI : TurretAIBase
{
    protected override bool OperateAttack()
    {
        var mon = _enemys[0].GetComponent<Monster>();
        if (mon.isAlive)
        {
            mon.Hit(_data.Damage, out bool isDie);
            _currentAttackWait = 0;
            _animator.SetTrigger(_attackAniHash);
            Array.ForEach(_paricles,(x)=>x.Play());
            int SoundIdx = Random.Range(0, _data._shotSound.Length);
            SoundManager.PlayClip( _data._shotSound[SoundIdx],transform.position);
            if (isDie)
                _enemys.Remove(mon.gameObject);
            return true;
        }

        return false;
    }

    protected override void LookAtEnemy()
    {
        Vector3 enemyPosition = new Vector3(_enemys[0].transform.position.x, 0, _enemys[0].transform.position.z);
        _head.LookAt(enemyPosition);
    }
}
