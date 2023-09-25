using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Stat")]
    public int health;
    public GameObject[] drops;
    public Transform spawnPosition;
    public Transform targetPosition;

    private NavMeshAgent _agent;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void OnEnable()
    {
        _agent.SetDestination(targetPosition.position);
    }

    private void OnDisable()
    {
        gameObject.transform.position = spawnPosition.position;
    }
}
