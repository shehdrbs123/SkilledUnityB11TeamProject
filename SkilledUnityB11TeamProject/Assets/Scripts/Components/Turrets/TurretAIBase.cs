using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class TurretAIBase : MonoBehaviour
{
    [SerializeField] protected Transform _head;
    [SerializeField] protected TurretDataSO _data;
    [SerializeField] private GameObject _rangeObject;
    [SerializeField] protected ParticleSystem[] _paricles;
    [SerializeField] protected float _rotateSpeed = 0;
    protected Animator _animator;
    protected List<GameObject> _enemys;
    protected float _currentAttackWait;
    protected int _attackAniHash;
    
    private SphereCollider _rangeCols;
    public RangeDraw RangeRenderer { get; private set; }

   

    protected virtual void Awake()
    {
        _enemys = new List<GameObject>();
        _animator = GetComponentInChildren<Animator>();
        _rangeCols = _rangeObject.GetComponent<SphereCollider>();
        RangeRenderer = _rangeObject.GetComponentInChildren<RangeDraw>();

        _rangeCols.radius = _data.halfRadius;
        RangeRenderer.radius = _data.halfRadius;
        _attackAniHash = Animator.StringToHash("IsAttack");
        if(_rotateSpeed == 0 ) _rotateSpeed = Random.Range(0.5f, 1f);
    }

    protected virtual void Update()
    {
        if (_currentAttackWait < _data.attackRate)
            _currentAttackWait += Time.deltaTime;
    }

    protected virtual void OnDisable()
    {
        RangeRenderer.gameObject.SetActive(true);
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
                if (OperateAttack())
                    _currentAttackWait = 0;
            }
        }
        else
        {
            RotateIdle();
        }
    }

    protected virtual void RotateIdle()
    {
        _head.transform.Rotate(0f,_rotateSpeed,0f);
    }

    protected abstract bool OperateAttack();

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

    public string GetTurretInfo()
    {
        StringBuilder sb = new StringBuilder(100);
        string result;
        sb.Append("이름 : ").Append(_data.TurretName).Append("\n");
        sb.Append("범위 : ").Append(_data.halfRadius).Append("\n");
        sb.Append("공격속도 : ").Append(_data.attackRate).Append("\n");
        sb.Append("공격력 : ").Append(_data.Damage).Append("\n");
        result = sb.ToString();
        sb = null;
        return result;
    }
}
