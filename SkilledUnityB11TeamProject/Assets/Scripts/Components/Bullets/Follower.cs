using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    //[SerializeField] private float degreeDelta=0.5f;
    [SerializeField] private float adjustDirectionForce = 7f;
    private Transform target;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Init(Transform target,float shotSpeed)
    {
        this.target = target;
        _rigidBody.AddForce(transform.forward * shotSpeed,ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        if (!target.gameObject.activeSelf)
            gameObject.SetActive(false);
        Vector3 distance = target.position - transform.position;
        if (_rigidBody.velocity != Vector3.zero) 
            transform.rotation =  Quaternion.LookRotation(_rigidBody.velocity);
        
        Vector3 targetDirection = distance.normalized;
        float dot = Vector3.Dot(targetDirection, _rigidBody.velocity.normalized);
        
        _rigidBody.AddForce(targetDirection*adjustDirectionForce);
    }
}