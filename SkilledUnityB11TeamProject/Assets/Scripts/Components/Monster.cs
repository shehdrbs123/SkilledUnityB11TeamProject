using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Stat")]
    public MonsterDataSO data;
    [SerializeField] private int _nowHP;        // 인스펙터에서 확인용 직렬화. 추후 제거
    public bool isAlive = true;

    private NavMeshAgent _agent;
    private Animator _animator;
    private SkinnedMeshRenderer _meshRenderers;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _meshRenderers = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnEnable()
    {
        isAlive = true;
        _nowHP = data.hp;
        _meshRenderers.material = data.material;
        _agent.enabled = true;
        _agent.speed = data.speed;
        _agent.isStopped = false;
        gameObject.transform.position = data.SPAWN_POSITION;
        gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);

        _agent.SetDestination(data.TARGET_POSITION);
    }

    private void Update()
    {
        if (isAlive && _agent.remainingDistance <= 0)
        {
            StartCoroutine(CoAttack());
        }
    }

    public void Hit(int damage, out bool die)
    {
        die = false;
        if (!isAlive)
            return;

        _nowHP -= damage;

        if (_nowHP <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(CoDie());
            die = true;
        }
        else
        {
            StartCoroutine(CoHitAnimation());
        }
    }


    private IEnumerator CoHitAnimation()
    {
        _agent.isStopped = true;

        _animator.SetTrigger(data.ANIM_HIT);
        _meshRenderers.material.color = Color.red;

        yield return data.DELAY_HIT;

        _agent.isStopped = false;
        _meshRenderers.material.color = Color.white;
    }

    public IEnumerator CoDie()
    {
        isAlive = false;
        _agent.isStopped = true;
        _agent.enabled = false;

        _animator.SetTrigger(data.ANIM_DIE);

        foreach (ItemData item in data.dropResources)
        {
            GameManager.Instance.ResourceDisplayUI.ShowGetResource(item);
            GameManager.Instance.inventory.AddItem(item);
        }

        yield return data.DELAY_DIE;

        StartCoroutine(CoDisapear());        
    }

    private IEnumerator CoDisapear()
    {
        while (gameObject.transform.position.y >= -5)
        {
            transform.Translate(Vector3.down * 0.05f);
            yield return null;
        }

        UnActive();
    }

    private IEnumerator CoAttack()
    {
        isAlive = false;
        _agent.isStopped = true;

        _animator.SetTrigger(data.ANIM_ATTACK);

        yield return data.DELAY_ATTACK;

        GameManager.Instance._conditionManager.GetDamaged();

        UnActive();
    }

    private void UnActive()
    {
        gameObject.SetActive(false);
    }
}
