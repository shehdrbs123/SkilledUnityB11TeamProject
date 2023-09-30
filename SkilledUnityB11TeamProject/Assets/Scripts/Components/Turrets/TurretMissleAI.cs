using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretMissleAI : TurretAIBase
{
    [SerializeField] private int _missleCount;
    [SerializeField] private float _shootRate;
    [SerializeField] private Transform[] _shotPos;
    [SerializeField] private float _bulletSpeed = 5;
    private int _shotIdx;
    private Vector3 _targetDistance;
    private WaitForSeconds waitForNextShoot;

    private PrefabManager _prefabManager;
    
    protected override void Awake()
    {
        base.Awake();
        waitForNextShoot = new WaitForSeconds(_shootRate);
    }

    protected void Start()
    {
        _prefabManager = GameManager.Instance.prefabManager;
    }

    protected override bool OperateAttack()
    {
        StartCoroutine(ShootMissle());
        return true;
    }

    private IEnumerator ShootMissle()
    {
        for (int i = 0; i < _missleCount; ++i)
        {
            if (_enemys.Count <= 0)
                yield break;
            int target = Random.Range(0, _enemys.Count);
            
            GameObject bullet = _prefabManager.SpawnFromPool(PoolType.MissleBullet);
            bullet.SetActive(true);
            bullet.transform.SetPositionAndRotation(_shotPos[_shotIdx].position,_shotPos[_shotIdx].rotation);
            bullet.GetComponent<Follower>().Init(_enemys[target].transform,_bulletSpeed);

            _paricles[_shotIdx].Play();
            //_animator.SetTrigger(_attackAniHash);

            _shotIdx = (_shotIdx + 1) % _shotPos.Length;

            yield return waitForNextShoot;
        }
    }

    protected override void LookAtEnemy()
    {
        _targetDistance = _enemys[_enemys.Count/2].transform.position - _head.position; 
        _targetDistance = new Vector3(_targetDistance.x, 0, _targetDistance.z);
        _head.transform.rotation = Quaternion.RotateTowards(_head.transform.rotation,Quaternion.LookRotation(_targetDistance), 0.5f);
    }
}
