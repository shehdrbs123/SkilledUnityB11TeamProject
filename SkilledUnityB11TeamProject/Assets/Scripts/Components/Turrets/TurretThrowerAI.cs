using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class TurretThrowerAI : TurretAIBase
{
    [Header("조정 장치")]
    [SerializeField] private Transform _shotPos;
    [SerializeField] private Transform _barrel;
    [SerializeField] private float _ToleranceBarrelAngle;
    [SerializeField] private float _ToleranceHeadAngle;
    [SerializeField] private float _barrelRotateSpeed;
    [SerializeField] private bool isLineDraw;
    
    
    [Header("발사 오브젝트")] [SerializeField] private PoolType bulletType;
    private PrefabManager _prefabManager;
    private float _bulletSpeed;
    private float _totalTime;
    private int _positionCount;

    private Vector3 _targetDistance;
    private Vector3 _sightAlign;
    private LineRenderer _bulletMoveLine;
    private void Start()
    {
        float re = _data.halfRadius - _shotPos.position.y * 1.45f;
        _bulletSpeed = Mathf.Sqrt(re * 9.8f) ;
        _totalTime = re / 9.8f;
        _positionCount = Mathf.CeilToInt(_totalTime / Time.fixedDeltaTime);
        _prefabManager = GameManager.Instance.prefabManager;
        _bulletMoveLine = GetComponent<LineRenderer>();
        _bulletMoveLine.positionCount = _positionCount;
    }
    //이건 아까워서 남깁니다... 포물선 그려주는 부분.....(안써도 되긴하지만)
#if UNITY_EDITOR
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isLineDraw)
        {
            for (int i = 0; i < _positionCount; ++i)
            {
                float angle = (360-_barrel.eulerAngles.x) * Mathf.Deg2Rad;
                float deltaTime = i * Time.fixedDeltaTime;
                float x = _bulletSpeed * Mathf.Cos(angle) * deltaTime;
                float y = _bulletSpeed * Mathf.Sin(angle) * deltaTime - (0.5f * 9.8f * deltaTime * deltaTime);
            
                Vector3 distance = _head.forward * x;
            
                _bulletMoveLine.SetPosition(i,new Vector3(distance.x,y,distance.z)+_shotPos.transform.position);
            }
        }
        
    }
#endif
    protected override bool OperateAttack()
    {
        if (isShotOk())
        {
            Shoot();
            _animator.SetTrigger(_attackAniHash);
            return true;
        }

        return false;
    }

    private void Shoot()
    {
        GameObject bullet = _prefabManager.SpawnFromPool(bulletType);
        bullet.transform.SetPositionAndRotation(_shotPos.position,transform.rotation);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(_shotPos.forward*_bulletSpeed,ForceMode.VelocityChange);
        
        SoundManager.PlayRandomClip( _data._shotSound,transform.position);
        
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
        _head.transform.rotation = Quaternion.RotateTowards(_head.rotation, Quaternion.LookRotation(_targetDistance), _rotateSpeed);
    }

    private void SightAlign()
    {
        float distanceLength = _targetDistance.magnitude;
        float cos = distanceLength / (_bulletSpeed * _totalTime);
        float angle = Mathf.Acos(cos)*Mathf.Rad2Deg;
        //Debug.Log(angle);
        if (angle == 0 || float.IsNaN(angle))
            return;
        _sightAlign = new Vector3(-angle, _barrel.eulerAngles.y, _barrel.eulerAngles.z);
        _barrel.transform.rotation = Quaternion.RotateTowards(_barrel.rotation, Quaternion.Euler(_sightAlign), _barrelRotateSpeed);
    }

    private bool isShotOk()
    {
        Vector3 targetDirection = _targetDistance.normalized;
        Vector3 forward = new Vector3(_head.forward.x, 0, _head.forward.z);
        float LookisCorrect = Vector3.Dot(forward, targetDirection);
        float LookAngle = Mathf.Acos(LookisCorrect) * Mathf.Rad2Deg;
        if (LookAngle > _ToleranceHeadAngle)
            return false;
        
        float sightAngle = _sightAlign.x;
        float barrelAngle = _barrel.eulerAngles.x;
        if (360 + sightAngle - barrelAngle > _ToleranceBarrelAngle)
            return false;
        
        return true;
    }
}
