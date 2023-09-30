using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageRange : MonoBehaviour
{
    [SerializeField] private DamageRangeDataSO data;

    private float _currentDuration;
    private float _fireDamageTime;

    private List<Monster> _monsters;

    private void Awake()
    {
        _monsters = new List<Monster>(40);
    }

    private void Start()
    {
        transform.localScale = new Vector3(data.explosionRadius,0,data.explosionRadius);
    }
    
    private void Update()
    {
        if (_currentDuration >= data.duration)
        {
            gameObject.SetActive(false);
            _monsters.Clear();
            return;
        }

        if (_fireDamageTime >= data.damageRate)
        {
            for (int i = 0; i < _monsters.Count; ++i)
            {
                if(_monsters[i].isAlive)
                    _monsters[i].Hit((int)data.damage,out bool die);
                else
                {
                    _monsters.RemoveAt(i);
                    i--;
                }
            }

            _fireDamageTime = 0;
        }
        
        _currentDuration += Time.deltaTime;
        _fireDamageTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        _monsters.Add(other.GetComponent<Monster>());
    }

    private void OnTriggerExit(Collider other)
    {
        _monsters.Remove(other.GetComponent<Monster>());
    }
}
