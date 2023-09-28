using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretsAI : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private TurretDataSO _data;
    [SerializeField] private GameObject _rangeObject;
    [SerializeField] private ParticleSystem[] paricles;

    private Animator _animator;
    private SphereCollider _rangeCols;
    private CircleDraw _rangeRenderer;
    
    private List<GameObject> _enemys;
    private float _currentAttackWait;

    private readonly int attackAniHash = Animator.StringToHash("IsAttack");

    private void Awake()
    {
        _enemys = new List<GameObject>();
        _animator = GetComponent<Animator>();
        _rangeCols = _rangeObject.GetComponent<SphereCollider>();
        _rangeRenderer = _rangeObject.GetComponent<CircleDraw>();

        _rangeCols.radius = _data.halfRadius;
        _rangeRenderer.radius = _data.halfRadius;
    }

    private void Update()
    {
        if (_currentAttackWait < _data.attackRate)
        {
            _currentAttackWait += Time.deltaTime;
        }
        else
        {
            if (_enemys.Count > 0)
            {
                OperateAttack();
            }
        }    
    }

    private void FixedUpdate()
    {
        if (_enemys.Count > 0)
        {
            LookAtEnemy();
        }
    }

    protected virtual void OperateAttack()
    {
        var mon = _enemys[0].GetComponent<Monster>();
        mon.Hit(_data.Damage, out bool isDie);
        _currentAttackWait = 0;
            
        _animator.SetTrigger(attackAniHash);
        Array.ForEach(paricles,(x)=>x.Play());
        if (isDie)
            _enemys.Remove(mon.gameObject);
    }

    protected virtual void LookAtEnemy()
    {
        Vector3 enemyPosition = new Vector3(_enemys[0].transform.position.x, 0, _enemys[0].transform.position.z);
        _head.LookAt(enemyPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Monster _))
        {
            _enemys.Add(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Monster _))
        {
            _enemys.Remove(other.gameObject);
        }
    }
}
