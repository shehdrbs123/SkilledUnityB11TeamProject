using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class TurretHowitzerAI : TurretAIBase
{
    [SerializeField] private Transform ShotPos;
    [SerializeField] private LineRenderer testLine;
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform barrel;
    [SerializeField] private float speed;
    [SerializeField] private GameObject TestBullet;
    private float totalTime;
    private int positionCount;
    private void Start()
    {
        float re = _data.halfRadius - ShotPos.position.y * 1.45f;
        speed = Mathf.Sqrt(re * 9.8f) ;
        totalTime = re / 9.8f;
        positionCount = Mathf.CeilToInt(totalTime / Time.fixedDeltaTime);
        testLine.positionCount = positionCount;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        for (int i = 0; i < positionCount; ++i)
        {
            float angle = (360-barrel.eulerAngles.x) * Mathf.Deg2Rad;
            float deltaTime = i * Time.fixedDeltaTime;
            float x = speed * Mathf.Cos(angle) * deltaTime;
            float y = speed * Mathf.Sin(angle) * deltaTime - (0.5f * 9.8f * deltaTime * deltaTime);
            
            Vector3 distance = _head.forward * x;
            
            testLine.SetPosition(i,new Vector3(distance.x,y,distance.z)+ShotPos.transform.position);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(TestBullet, ShotPos.position, ShotPos.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(ShotPos.forward*speed,ForceMode.VelocityChange);
        }
    }
    protected override void OperateAttack()
    {
        Vector3 distance = _enemys[0].transform.position - _head.position;
        float distanceLength = distance.magnitude;
        float cos = distanceLength / (speed * totalTime);
        float angle = Mathf.Acos(cos)*Mathf.Rad2Deg; 
        Vector3 eulerAngle = new Vector3(-angle, _head.eulerAngles.y, _head.eulerAngles.z);
        barrel.transform.rotation = Quaternion.Euler(eulerAngle);
    }

    protected override void LookAtEnemy()
    {
        Vector3 targetDir = _enemys[0].transform.position - _head.position;
        targetDir = new Vector3(targetDir.x, 0, targetDir.z);
        _head.transform.rotation = Quaternion.RotateTowards(_head.rotation, Quaternion.LookRotation(targetDir), 0.5f);
    }
}
