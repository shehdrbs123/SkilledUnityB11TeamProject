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
    private readonly int IsHIt = Animator.StringToHash("IsHit");
    private readonly WaitForSeconds hitDelay = new WaitForSeconds(0.75f);

    private bool isAlive = true;
    private bool isInvincible = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _meshRenderers = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnEnable()
    {
        isAlive = true;
        _meshRenderers.material = data.material;
        _nowHP = data.hp;
        _agent.speed = data.speed;
        gameObject.transform.position = data.SPAWN_POSITION;
        gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);

        _agent.SetDestination(data.TARGET_POSITION);
    }

    public void Hit(int damage)
    {
        if (isInvincible && isAlive)
            return;

        _nowHP -= damage;

        if (_nowHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(CoHitAnimation());
        }
    }

    private IEnumerator CoHitAnimation()
    {
        isInvincible = true;
        _agent.isStopped = true;
        _animator.SetTrigger(IsHIt);
        _meshRenderers.material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return hitDelay;

        isInvincible = false;
        _agent.isStopped = false;
        _meshRenderers.material.color = Color.white;
    }

    public void Die()
    {
        isAlive = false;
        _agent.isStopped = true;
        _animator.SetTrigger(IsDie);
        Invoke(nameof(ReturnToSpawnPoint), 2);
    }

    private void ReturnToSpawnPoint()
    {
        gameObject.SetActive(false);
    }
}
