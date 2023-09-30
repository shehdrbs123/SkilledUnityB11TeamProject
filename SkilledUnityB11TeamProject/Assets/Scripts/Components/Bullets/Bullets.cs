using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Bullets : MonoBehaviour
{
    [SerializeField] protected BulletDataSO _bulletData;


    protected PrefabManager _prefabManager;
    
    private void Start()
    {
        _prefabManager = GameManager.Instance.prefabManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayDestroyParticle();
        Explosion();
        PlaySound();
        gameObject.SetActive(false);
    }

    protected virtual void Explosion()
    {
        Collider[] recognizedMonster = Physics.OverlapSphere(transform.position, _bulletData.explosionRadius, _bulletData.targetLayerMask);

        for (int i = 0; i < recognizedMonster.Length; ++i)
        {
            Vector3 distance = recognizedMonster[i].transform.position - transform.position;
            float damageRate = distance.magnitude / _bulletData.explosionRadius;
            Monster monster = recognizedMonster[i].gameObject.GetComponent<Monster>();
            monster.Hit((int)(damageRate * _bulletData.damage),out bool isDie);
        }
    }

    protected void PlaySound()
    {
        SoundManager.PlayRandomClip(_bulletData.HitSound,transform.position);
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
        Gizmos.DrawWireSphere(transform.position,_bulletData.explosionRadius);
    }
}
