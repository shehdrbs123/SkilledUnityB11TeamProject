using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    //[SerializeField] private float degreeDelta=0.5f;
    [SerializeField] private float adjustDirectionForce = 7f;
    
    private List<GameObject> targets;
    private Monster target;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Init(List<GameObject> targets,float shotSpeed)
    {
        this.targets = targets;
        target = chooseTarget();
        _rigidBody.AddForce(transform.forward * shotSpeed,ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        if (target==null || !target.isAlive)
        {
            target = chooseTarget();
            if (target == null)
            {
                _rigidBody.AddForce(Vector3.down*10f);
                //gameObject.SetActive(false);
                return;
            }
        }

        Vector3 distance = target.gameObject.transform.position - transform.position;
        if (_rigidBody.velocity != Vector3.zero) 
            transform.rotation =  Quaternion.LookRotation(_rigidBody.velocity);
        
        Vector3 targetDirection = distance.normalized;

        _rigidBody.AddForce(targetDirection*adjustDirectionForce);
    }

    private Monster chooseTarget()
    {
        if (targets.Count <= 0)
            return null;
        int random = Random.Range(0, targets.Count);
        return targets[random].GetComponent<Monster>();
    }
}