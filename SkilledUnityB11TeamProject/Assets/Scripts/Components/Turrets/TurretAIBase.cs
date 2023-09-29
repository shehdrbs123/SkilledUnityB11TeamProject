using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    private void Awake()
    {
        _enemys = new List<GameObject>();
        _animator = GetComponent<Animator>();
        _rangeCols = _rangeObject.GetComponent<SphereCollider>();
        _rangeRenderer = _rangeObject.GetComponentInChildren<RangeDraw>();

        _rangeCols.radius = _data.halfRadius;
        _rangeRenderer.radius = _data.halfRadius;
        _attackAniHash = Animator.StringToHash("IsAttack");
    }

    protected virtual void Update()
    {
        if (_currentAttackWait < _data.attackRate)
            _currentAttackWait += Time.deltaTime;
    }

    protected virtual void OnDisable()
    {
        _rangeRenderer.gameObject.SetActive(true);
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
    }

    protected abstract void OperateAttack();

    protected abstract void LookAtEnemy();

    private void CheckDie()
    {
        for (int i = 0; i < _enemys.Count; ++i)
        {
            if (!_enemys[i].GetComponent<Monster>().isAlive)
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
