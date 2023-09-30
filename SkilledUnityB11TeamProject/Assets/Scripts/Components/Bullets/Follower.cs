using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    private float degreeDelta=0.5f;
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
        if (!target)
            gameObject.SetActive(false);
        Vector3 distance = target.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(distance),degreeDelta);
        _rigidBody.AddForce(distance.normalized);
    }
}