using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Stat")]
    public MonsterDataSO data;
    [SerializeField] private int _nowHP;        // 인스펙터에서 확인용 직렬화. 추후 제거

    private NavMeshAgent _agent;
    private Animator _animator;
    private SkinnedMeshRenderer _meshRenderers;

    private readonly int IsDie = Animator.StringToHash("IsDie");

    private bool isAlive;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _meshRenderers = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnEnable()
    {
        _meshRenderers.material = data.material;
        _nowHP = data.hp;
        _agent.speed = data.speed;
        gameObject.transform.position = data.spawnPosition;
        gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);

        isAlive = true;
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
