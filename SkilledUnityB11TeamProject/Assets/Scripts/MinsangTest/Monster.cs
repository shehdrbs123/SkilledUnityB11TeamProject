using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Stat")]
    public MonsterDataSO data;
    [SerializeField] private int _nowHP;        // 인스펙터에서 확인하려고 직렬화 

    private NavMeshAgent _agent;
    private Animator _animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private readonly int IsDie = Animator.StringToHash("IsDie");

    private bool isAlive;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void OnEnable()
    {
        isAlive = true;
        _nowHP = data.hp;
        _agent.speed = data.speed;
        gameObject.transform.position = data.spawnPosition;

        gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);

        _agent.SetDestination(data.targetPosition);
        InvokeRepeating(nameof(TEST), 0f, 1f);
    }

    private void Update()
    {
        if (_nowHP <= 0 && isAlive)
            Die();
    }

    private void TEST()
    {
        _nowHP -= 1;
    }

    public void Die()
    {
        isAlive = false;
        _agent.isStopped = true;
        _animator.SetTrigger(IsDie);
        CancelInvoke(nameof(TEST));
        Invoke(nameof(ReturnToSpawnPoint), 2);
    }

    private void ReturnToSpawnPoint()
    {
        gameObject.SetActive(false);
    }
}
