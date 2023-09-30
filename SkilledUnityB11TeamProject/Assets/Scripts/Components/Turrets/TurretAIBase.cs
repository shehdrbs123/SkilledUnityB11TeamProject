using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class TurretAIBase : MonoBehaviour
{
    [SerializeField] protected Transform _head;
    [SerializeField] protected TurretDataSO _data;
    [SerializeField] private GameObject _rangeObject;
    [SerializeField] protected ParticleSystem[] _paricles;
    protected Animator _animator;
    protected List<GameObject> _enemys;
    protected float _currentAttackWait;
    protected int _attackAniHash;
    
    private SphereCollider _rangeCols;
    private RangeDraw _rangeRenderer;

    private float idleRotateSpeed;

    protected virtual void Awake()
    {
        _enemys = new List<GameObject>();
        _animator = GetComponent<Animator>();
        _rangeCols = _rangeObject.GetComponent<SphereCollider>();
        _rangeRenderer = _rangeObject.GetComponentInChildren<RangeDraw>();

        _rangeCols.radius = _data.halfRadius;
        _rangeRenderer.radius = _data.halfRadius;
        _attackAniHash = Animator.StringToHash("IsAttack");
        idleRotateSpeed = Random.Range(0.5f, 1f);
    }

    protected virtual void Update()
    {
        if (_currentAttackWait < _data.attackRate)
            _currentAttackWait += Time.deltaTime;
    }

    protected virtual void OnDisable()
    {
        _rangeRenderer.gameObject.SetActive(true);
        enabled = false;
    }

    protected virtual void FixedUpdate()
    {
        CheckDie();
        if (_enemys.Count > 0)
        {
            LookAtEnemy();
            if (_currentAttackWait >= _data.attackRate)
            {
                _currentAttackWait = 0;
                OperateAttack();
            }
        }
        else
        {
            RotateIdle();
        }
    }

    protected virtual void RotateIdle()
    {
        _head.transform.Rotate(0f,idleRotateSpeed,0f);
    }

    protected abstract void OperateAttack();

    protected abstract void LookAtEnemy();
    
    

    private void CheckDie()
    {
        for (int i = 0; i < _enemys.Count; ++i)
        {
            Monster mon = _enemys[i].GetComponent<Monster>();
            if (mon != null && !mon.isAlive)
            {
                _enemys.Remove(_enemys[i]);
                --i;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _enemys.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _enemys.Remove(other.gameObject);
    }
}
