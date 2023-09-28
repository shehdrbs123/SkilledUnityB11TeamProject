using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TurretHowitzerAI : TurretAIBase
{
    [SerializeField] private Transform ShotPos;
    [SerializeField] private LineRenderer test;
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform barrel;
    [SerializeField]private float speed = 10f;

    private void Start()
    {
        test.positionCount = 1000;
        test.SetPosition(0,ShotPos.position);
    }

    void FixedUpdate()
    {
        float speedToSeconds = speed*Time.fixedDeltaTime;
        for (int i = 0; i<test.positionCount; ++i)
        {
            float angle = (360-barrel.eulerAngles.x) * Mathf.Deg2Rad;
            float x = speedToSeconds * Mathf.Cos(angle) * i;
            float y = speedToSeconds * Mathf.Sin(angle)*i - (0.5f * 0.98f * i * i);

            
            Vector3 distance = transform.forward * x;
            
            test.SetPosition(i,new Vector3(distance.x,y,distance.z)*0.01f + ShotPos.transform.position);
        }
    }
    protected override void OperateAttack()
    {
        //Debug.Log(_head.localEulerAngles);
    }

    protected override void LookAtEnemy()
    {
        Vector3 targetDir = _enemys[0].transform.position - _head.position;
        _head.transform.rotation = Quaternion.RotateTowards(_head.rotation, Quaternion.LookRotation(targetDir), 0.5f);
    }
}
