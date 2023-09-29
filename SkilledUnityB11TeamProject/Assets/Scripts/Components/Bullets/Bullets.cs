using System;
using UnityEngine;
using UnityEngine.Serialization;


public class Bullets : MonoBehaviour
{
    [SerializeField]private LayerMask targetLayerMask;
    [FormerlySerializedAs("explosionHalfRadius")] [SerializeField]private float explosionRadius;
    [SerializeField]private float damage;
    private PrefabManager _prefabManager;
    
    private void Start()
    {
        _prefabManager = GameManager.Instance.prefabManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayDestroyParticle();
        Explosion();
        gameObject.SetActive(false);
    }

    private void Explosion()
    {
        Collider[] recognizedMonster = Physics.OverlapSphere(transform.position, explosionRadius, targetLayerMask);

        for (int i = 0; i < recognizedMonster.Length; ++i)
        {
            Debug.Log(recognizedMonster[i].name);
            Vector3 distance = recognizedMonster[i].transform.position - transform.position;
            float damageRate = distance.magnitude / explosionRadius;
            Monster monster = recognizedMonster[i].gameObject.GetComponent<Monster>();
            monster.Hit((int)(damageRate * damage),out bool isDie);
        }
    }

    private void PlayDestroyParticle()
    {
        GameObject obj = _prefabManager.SpawnFromPool(PoolType.BangParticle);
        obj.transform.position = transform.position;
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        obj.SetActive(true);
        ps.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
