using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class TurretHowitzerAI : TurretAIBase
{
    [Header("조정 장치")]
    [SerializeField] private Transform _shotPos;
    [SerializeField] private Transform _barrel;
    [SerializeField] private float _ToleranceBarrelAngle;
    [SerializeField] private float _ToleranceLookDirectionAngle;

    private PrefabManager _prefabManager;
    private float _bulletSpeed;
    private float _totalTime;
    private int _positionCount;

    private Vector3 _targetDistance;
    private Vector3 _sightAlign;
    private void Start()
    {
        float re = _data.halfRadius - _shotPos.position.y * 1.45f;
        _bulletSpeed = Mathf.Sqrt(re * 9.8f) ;
        _totalTime = re / 9.8f;
        _positionCount = Mathf.CeilToInt(_totalTime / Time.fixedDeltaTime);
        _prefabManager = GameManager.Instance.prefabManager;
    }
    // 이건 아까워서 남깁니다... 포물선 그려주는 부분.....(안써도 되긴하지만)
// #if UNITY_EDITOR
//     protected override void FixedUpdate()
//     {
//         base.FixedUpdate();
//         
//         for (int i = 0; i < _positionCount; ++i)
//         {
//             float angle = (360-_barrel.eulerAngles.x) * Mathf.Deg2Rad;
//             float deltaTime = i * Time.fixedDeltaTime;
//             float x = _bulletSpeed * Mathf.Cos(angle) * deltaTime;
//             float y = _bulletSpeed * Mathf.Sin(angle) * deltaTime - (0.5f * 9.8f * deltaTime * deltaTime);
//             
//             Vector3 distance = _head.forward * x;
//             
//             _bulletMoveLine.SetPosition(i,new Vector3(distance.x,y,distance.z)+_shotPos.transform.position);
//         }
//     }
// #endif
    protected override void OperateAttack()
    {
        if (isShotOk())
        {
            Shoot();
            _animator.SetTrigger(_attackAniHash);
        }
    }

    private void Shoot()
    {
        GameObject bullet = _prefabManager.SpawnFromPool(PoolType.HowitzerBullet);
        bullet.transform.SetPositionAndRotation(-_shotPos.position,transform.rotation);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(_shotPos.forward*_bulletSpeed,ForceMode.VelocityChange);
        
        int SoundIdx = Random.Range(0, _data._shotSound.Length);
        SoundManager.PlayClip( _data._shotSound[SoundIdx],transform.position);
        
        Array.ForEach(_paricles,(x) => x.Play());
    }

    protected override void LookAtEnemy()
    { 
       _targetDistance = _enemys[0].transform.position - _head.position; 
       _targetDistance = new Vector3(_targetDistance.x, 0, _targetDistance.z);
       RotateBody();
       SightAlign();
    }

    private void RotateBody()
    {
        _head.transform.rotation = Quaternion.RotateTowards(_head.rotation, Quaternion.LookRotation(_targetDistance), 0.5f);
    }

    private void SightAlign()
    {
        float distanceLength = _targetDistance.magnitude;
        float cos = distanceLength / (_bulletSpeed * _totalTime);
        float angle = Mathf.Acos(cos)*Mathf.Rad2Deg;
        _sightAlign = new Vector3(-angle, _barrel.eulerAngles.y, _barrel.eulerAngles.z);
        _barrel.transform.rotation = Quaternion.RotateTowards(_barrel.rotation, Quaternion.Euler(_sightAlign), 0.5f);
    }

    private bool isShotOk()
    {
        Vector3 targetDirection = _targetDistance.normalized;
        Vector3 forward = new Vector3(_head.forward.x, 0, _head.forward.z);
        float LookisCorrect = Vector3.Dot(forward, targetDirection);
        float LookAngle = Mathf.Acos(LookisCorrect) * Mathf.Rad2Deg;
        if (LookAngle > _ToleranceLookDirectionAngle)
            return false;

        float sightAngle = _sightAlign.x;
        float barrelAngle = _barrel.eulerAngles.x;
        if (360 + sightAngle - barrelAngle > _ToleranceBarrelAngle)
            return false;
        
        return true;
    }
}
